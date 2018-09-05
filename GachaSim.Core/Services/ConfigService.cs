using GachaSim.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GachaSim.Core.Services
{
	public class ConfigService
	{
		private const string DIR_CFG = "config";

		private readonly string ConfigDirectory;
		private readonly IOutputService _outputService;

		private Dictionary<string, IGacha> Gachas = new Dictionary<string, IGacha>();
		private Dictionary<string, Pull> Pulls = new Dictionary<string, Pull>();

		public ConfigService(IOutputService outputService)
		{
			this._outputService = outputService;
			this.ConfigDirectory = this.InitializeConfigDirectory();
			this.LoadConfigurationFiles();
		}

		public List<IGacha> GetAllGachas() => this.Gachas.Values.ToList();
		public List<Pull> GetAllPulls() => this.Pulls.Values.ToList();

		public IGacha GetGacha(string code) => this.Gachas.ContainsKey(code) ? this.Gachas[code] : null;
		public Pull GetPull(string code) => this.Pulls.ContainsKey(code) ? this.Pulls[code] : null;

		private string InitializeConfigDirectory()
		{
			var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DIR_CFG);

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			return dir;
		}

		private void LoadConfigurationFiles()
		{
			string[] bannerConfigs = Directory.GetFiles(this.ConfigDirectory, "*.banner.json", SearchOption.TopDirectoryOnly);
			string[] stepupConfigs = Directory.GetFiles(this.ConfigDirectory, "*.stepup.json", SearchOption.TopDirectoryOnly);
			string[] pullConfigs = Directory.GetFiles(this.ConfigDirectory, "*.pull.json", SearchOption.TopDirectoryOnly);

			this.LoadBanners(bannerConfigs);
			this.LoadStepups(stepupConfigs);
			this.LoadPulls(pullConfigs);
		}

		private void LoadBanners(params string[] bannerConfigs)
		{
			foreach (var banner in Defaults.Gachas)
			{
				banner.IsDefault = true;
				this.Gachas.Add(banner.Code, banner);
			}

			foreach (var banner in this.DeserializeFromJsonFiles<Banner>(bannerConfigs))
			{
				if (this.Gachas.ContainsKey(banner.Code))
				{
					this._outputService.WriteLine("WARNING: \"{0}\" Banner was not loaded due because the code '{1}' is already used in another banner", banner.Name, banner.Code);
				}
				else
				{
					this.Gachas.Add(banner.Code, banner);
				}
			}
		}

		private void LoadStepups(params string[] stepupConfigs)
		{			
			foreach (var banner in this.DeserializeFromJsonFiles<StepUp>(stepupConfigs))
			{
				if (this.Gachas.ContainsKey(banner.Code))
				{
					this._outputService.WriteLine("WARNING: \"{0}\" Banner was not loaded due because the code '{1}' is already used in another banner", banner.Name, banner.Code);
				}
				else
				{
					this.Gachas.Add(banner.Code, banner);
				}
			}
		}

		private void LoadPulls(params string[] pullConfigs)
		{
			foreach (var pull in Defaults.PullTypes)
			{
				pull.IsDefault = true;
				this.Pulls.Add(pull.Code, pull);
			}

			foreach (var pull in this.DeserializeFromJsonFiles<Pull>(pullConfigs))
			{
				if (this.Pulls.ContainsKey(pull.Code))
				{
					this._outputService.WriteLine("WARNING: \"{0}\" Pull was not loaded due because the code '{1}' is already used in another pull", pull.Name, pull.Code);
				}
				else
				{
					this.Pulls.Add(pull.Code, pull);
				}
			}
		}

		private IEnumerable<T> DeserializeFromJsonFiles<T>(params string[] files)
			where T : class, IConfigurable
		{
			foreach (var file in files.Select(f => new FileInfo(f)))
			{
				T obj = default(T);

				try
				{
					var json = File.ReadAllText(file.FullName);
					obj = JsonConvert.DeserializeObject<T>(json);
					obj.IsDefault = false;

					if (string.IsNullOrEmpty(obj.Code))
					{
						throw new Exception($"No code defined in config file {file.Name}.  Configurables must have a code defined");
					}

					if (obj.Code.Length > 5)
					{
						throw new Exception($"Code cannot exceed 5 characters maximum. \"{obj.Code}\" is too long");
					}

					if (string.IsNullOrEmpty(obj.Name))
					{
						obj.Name = "Untitled";
					}
				}
				catch (Exception ex)
				{
					_outputService.WriteLine("WARNING: \"{0}\" {1} was not loaded due to a deserialization error: {2}", file.Name, typeof(T).Name, ex.Message);
					obj = null;
				}

				if (obj != null)
					yield return obj;
			}
		}
	}
}
