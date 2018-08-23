using System;
using System.Linq;
using System.Collections.Generic;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using System.Threading.Tasks;
using VaultSharp.V1.Commons;

namespace DotnetCore.VaultClient.Sample
{
    class Program
    {
        public const string VAULT_TOKEN = "myroot";
        public const string VAULT_URI = "http://localhost:8200";
        public static async Task Main(string[] args)
        {  
            var dev_values = new Dictionary<string, object>();
            dev_values.Add("Message", "Coming from Vault, i'm on DEV!");
            dev_values.Add("Info", new { Version = "1.0.0-development" });

            var test_values = new Dictionary<string, object>();
            test_values.Add("Message", "Coming from Vault, i'm on TEST!");
            test_values.Add("Info", new { Version = "1.0.0-test" });
            
            var client = GetVaultClient(VAULT_TOKEN, VAULT_URI);
            //Write sample data
            await WriteSecretAsync(client, "sampleapp,Development", dev_values);
            await WriteSecretAsync(client, "sampleapp,Test", test_values);

            //Read sample data
            var kv2_dev_secrets = await ReadSecretAsync(client, "sampleapp,Development");
            var kv2_test_secrets = await ReadSecretAsync(client, "sampleapp,Test");

            Console.WriteLine("\"Development\" secrets;");
            foreach (var item in kv2_dev_secrets?.Data?.Data)
            {
                Console.WriteLine($"Key: {item.Key} - Value: {item.Value}");
            }

            Console.WriteLine("\"Test\" secrets;");
            foreach (var item in kv2_test_secrets?.Data?.Data)
            {
                Console.WriteLine($"Key: {item.Key} - Value: {item.Value}");
            }
            Console.ReadKey();
        }

        public static IVaultClient GetVaultClient(string token, string vaultUri)
        {
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(token);
            var vaultClientSettings = new VaultClientSettings(vaultUri, authMethod);
            IVaultClient vaultClient = new VaultSharp.VaultClient(vaultClientSettings);
            return vaultClient;
        }

        public static async Task WriteSecretAsync(IVaultClient client, string path, IDictionary<string, object> data) => await client.V1.Secrets.KeyValue.V2.WriteSecretAsync(path, data);

        public static async Task<Secret<SecretData>> ReadSecretAsync(IVaultClient client, string path) => await client.V1.Secrets.KeyValue.V2.ReadSecretAsync(path);
    }
}
