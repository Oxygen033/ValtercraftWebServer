using Microsoft.Extensions.Options;
using MinecraftRconNet;
using System.Text.RegularExpressions;
using ValtercraftWebServer.Models;

namespace ValtercraftWebServer.Services
{
    public class RconService
    {
        private readonly RconSettings _settings;

        public RconService(IOptions<RconSettings> options)
        {
            _settings = options.Value;
        }

        public string SendCommand(string command)
        {
            using (var rcon = RconClient.INSTANCE)
            {
                rcon.SetupStream(_settings.Host, _settings.Port, _settings.Password);
                return rcon.SendMessage(RconMessageType.Command, command);
            }
        }

        public async Task<int?> GetOnline()
        {
            string response = SendCommand("list");
            if (response == null)
                return null;
            Match match = Regex.Match(response, @"\d+");
            if (match.Success)
            {
                return Convert.ToInt32(match.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
