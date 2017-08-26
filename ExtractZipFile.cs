public void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
{
        ZipFile zf = null;           
        FileStream fs = File.OpenRead(archiveFilenameIn);
        zf = new ZipFile(fs);
        if (!String.IsNullOrEmpty(password))
        {
			zf.Password = password;
        }
        foreach (ZipEntry zipEntry in zf)
        {
            if (!zipEntry.IsFile)
            {
                continue;
            }
            String entryFileName = zipEntry.Name;
            byte[] buffer = new byte[4096];
            Stream zipStream = zf.GetInputStream(zipEntry);
            String fullZipToPath = Path.Combine(outFolder, entryFileName);
            string directoryName = Path.GetDirectoryName(fullZipToPath);
            if (directoryName.Length > 0)
				Directory.CreateDirectory(directoryName);
            using (FileStream streamWriter = File.Create(fullZipToPath))
            {
                StreamUtils.Copy(zipStream, streamWriter, buffer);
            }
        }
        if (zf != null)
        {
            zf.IsStreamOwner = true;
            zf.Close();
        }

            
}