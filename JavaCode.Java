/*******************************************************************/
    // Based on code from user 'Michael'
    // https://stackoverflow.com/users/2262371/michael
    /*******************************************************************/


    public static void encryptAndDecryptAESJava() throws NoSuchPaddingException, NoSuchAlgorithmException,
            InvalidAlgorithmParameterException, InvalidKeyException,
            BadPaddingException, IllegalBlockSizeException {

        String clearText = "¿Que es obo?";
        String key = "aesEncryptionKey";//Warning: Must be 16 bytes long
        String iv = "encryptionIntVec"; //Warning: Must be 16 bytes long

        System.out.println("Clear text is: <" + clearText + ">");

        /*******************************************************************/
        // Encrypt
        /*******************************************************************/

        String cypherParameters = "AES/CBC/PKCS5Padding";
        Cipher encryptionCipher = Cipher.getInstance(cypherParameters);

        Charset CHARSET = Charset.forName("UTF-8");
        SecretKey secretKey = new SecretKeySpec(key.getBytes(CHARSET), "AES");
        IvParameterSpec ivParameterSpec = new IvParameterSpec(iv.getBytes(CHARSET));

        encryptionCipher.init(Cipher.ENCRYPT_MODE,secretKey ,ivParameterSpec);
        byte[] cipherText = encryptionCipher.doFinal(clearText.getBytes());

        String encryptedBase64Text = Base64.getEncoder().encodeToString(cipherText);

        System.out.println("EncryptedBase64Text: <" + encryptedBase64Text + ">");

        /*******************************************************************/
        // Decrypt
        /*******************************************************************/

        Cipher decryptionCipher = Cipher.getInstance(cypherParameters);

        decryptionCipher.init(Cipher.DECRYPT_MODE, secretKey, ivParameterSpec);

        byte[] decryptedTextAsByteArray = decryptionCipher.doFinal(Base64.getDecoder()
                .decode(encryptedBase64Text));

        String decryptedTextAsString = new String(decryptedTextAsByteArray, StandardCharsets.UTF_8);

        System.out.println("Decrypted cleartext: <" + decryptedTextAsString + ">");

    }
