using System;
using System.Linq;
using System.Collections.Generic;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using System.Threading.Tasks;

namespace DotnetCore.VaultClient.Sample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo("myroot");
            var vaultClientSettings = new VaultClientSettings("http://localhost:8200", authMethod);
            IVaultClient vaultClient = new VaultSharp.VaultClient(vaultClientSettings);
            //Write sample data
            var dev_values = new Dictionary<string, object>();
            dev_values.Add("Message", "Coming from Vault, i'm on DEV!");
            dev_values.Add("Info", new {Version = "1.0.0-development"});

            var test_values = new Dictionary<string, object>();
            test_values.Add("Message", "Coming from Vault, i'm on TEST!");
            test_values.Add("Info", new {Version = "1.0.0-test"});

            await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync("sampleapp,Development", dev_values);
            await vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync("sampleapp,Test", test_values);

            //Read sample data
            var kv2_dev_secrets = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("sampleapp,Development");
            var kv2_test_secrets = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync("sampleapp,Test");

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

    }
}
