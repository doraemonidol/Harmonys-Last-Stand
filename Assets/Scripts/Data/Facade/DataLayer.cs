using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Common;
using Common.Context;

namespace Data.Facade
{
    public class DataLayer : IDataFunc
    {
        private static DataLayer _layer;
        
        private static readonly object Lock = new object();

        private readonly string _key = "0706200330028071";

        private readonly string _iv = "1707080620200303";

        private DataLayer()
        {
        }
        
        public static DataLayer GetInstance()
        {
            if (_layer != null)
            {
                return _layer;
            }
            lock (Lock)
            {
                return _layer ??= new DataLayer();
            }
        }

        public void Save(Dictionary<string, object> args)
        {
            try
            {
                var sessionId = GameContext.GetInstance().SessionId;
                
                var pwd = System.IO.Directory.GetCurrentDirectory();
                
                var dir = System.IO.Directory.CreateDirectory(pwd 
                                                              + Paths.SESSION 
                                                              + "/" + sessionId);
                
                var target = (string)args["target"];
                
                var saveTarget = $"{dir}/{FilePathMapping(target)}";
                
                SaveEncryptedFile(saveTarget, (string)args["data"]);
            } catch (System.Exception e) {
                throw new System.Exception("Error: " + e.Message);
            }
        }

        public void Load(Dictionary<string, object> args)
        {
            try {
                var sessionID = (string) args["sessionID"];
                
                // get the current pwd
                var pwd = System.IO.Directory.GetCurrentDirectory();
                
                // Check if there is a session folder with the sessionID
                var dir = System.IO.Directory.CreateDirectory(pwd 
                                                              + Paths.SESSION 
                                                              + "/" + sessionID);

                var target = (string)args["target"];
                
                var loadTarget = $"{dir}/{FilePathMapping(target)}";

                if (!System.IO.File.Exists(loadTarget)) {
                    throw new System.Exception($"File not found for target {target}.");
                }
                
                var data = ReadEncryptedFile(loadTarget);
                    
                LoadIntoGameContext(sessionID, target, data);

            } catch (System.Exception e) {
                throw new System.Exception("Error: " + e.Message);
            }
        }

        private void LoadIntoGameContext(string session, string target, string data)
        {
            var cxt = GameContext.GetInstance();
            cxt.SessionId = session;
            cxt.Saved = false;
            switch (target)
            {
                case "weapon":
                    cxt.LoadWeapon(data);
                    break;
                case "stats":
                    cxt.LoadStats(data);
                    break;
                case "inventory":
                    cxt.LoadInventory(data);
                    break;
                case "gameplay":
                    cxt.LoadGameplay(data);
                    break;
                default:
                    throw new System.Exception("Unknown target for loading and saving.");
            }
        }
        
        private string ReadEncryptedFile(string path)
        {
            var file = System.IO.File.OpenText(path);
            var encryptedData = file.ReadToEnd();
            file.Close();
            return Decrypt(encryptedData);
        }
        
        private string Encrypt(string data)
        {
            using var aes = Aes.Create();
            aes.Key = System.Text.Encoding.UTF8.GetBytes(_key);
            aes.IV = System.Text.Encoding.UTF8.GetBytes(this._iv);
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var encrypted = encryptor.TransformFinalBlock(
                System.Text.Encoding.UTF8.GetBytes(data), 
                0, 
                data.Length
            );
            return System.Convert.ToBase64String(encrypted);
        }

        private string Decrypt(string data)
        {
            using var aes = Aes.Create();
            aes.Key = System.Text.Encoding.UTF8.GetBytes(this._key);
            aes.IV = System.Text.Encoding.UTF8.GetBytes(this._iv);
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var encrypted = System.Convert.FromBase64String(data);
            var decrypted = decryptor.TransformFinalBlock(
                encrypted, 
                0, 
                encrypted.Length
            );
            return System.Text.Encoding.UTF8.GetString(decrypted);
        }

        private void SaveEncryptedFile(string path, string data)
        {
            var file = System.IO.File.CreateText(path);
            file.Write(Encrypt(data));
            file.Close();
        }

        private static string FilePathMapping(string target)
        {
            return target switch
            {
                "weapon" => "1",
                "stats" => "2",
                "inventory" => "3",
                "gameplay" => "4",
                _ => throw new Exception("Unknown target for loading and saving.")
            };
        }
    }
}