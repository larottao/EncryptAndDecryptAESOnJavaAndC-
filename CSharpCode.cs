 /*******************************************************************/
        // Based on code from user 'Anonymous'
        // https://social.msdn.microsoft.com/profile/anonymous/?ws=usercard-mini
        /*******************************************************************/

        public static void encryptAndDecryptDotNet()
        {
            String originalString = "¿Que es obo?";

            String key = "aesEncryptionKey"; //Warning: Must be 16 bytes long
            String iv = "encryptionIntVec"; //Warning: must be 16 bytes long

            Console.WriteLine("Clear text is: <" + originalString + ">");

            String encryptedString = encrypt(originalString, key, iv);

            Console.WriteLine("EncryptedBase64Text: <" + encryptedString + ">");

            String decryptedString = decrypt(encryptedString, key, iv);

            Console.WriteLine("Decrypted cleartext: <" + decryptedString + ">");
        }

        private static AesManaged CreateAes(String key, String iv)
        {
            var aes = new AesManaged();

            aes.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aes.IV = System.Text.Encoding.UTF8.GetBytes(iv);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;

            return aes;
        }

        public static string encrypt(string text, string key, string iv)
        {
            using (AesManaged aes = CreateAes(key, iv))
            {
                ICryptoTransform encryptor = aes.CreateEncryptor();
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(text);

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string decrypt(string text, string key, string iv)
        {
            using (var aes = CreateAes(key, iv))
            {
                ICryptoTransform decryptor = aes.CreateDecryptor();
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
