using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Reflection;
using System.Net;
using System.Drawing.Imaging;
using System.Runtime.Serialization;

namespace Meow.FR.Rincevent.Core.Data
{
    static public class FileManager
    {
        /// <summary>
        /// Converts a stream to a byte array.
        /// </summary>
        /// <param name="stream">The stream that needs to be read.</param>
        static public byte[] StreamtoByteArray(Stream stream)
        {
            int offset = 0;
            int remaining = (int)stream.Length;
            byte[] data = new byte[remaining];
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException();
                remaining -= read;
                offset += read;
            }
            return data;
        }

        /// <summary>
        /// Converts a file to a byte array.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <returns></returns>
        static public byte[] FileToByteArray(string path)
        {
            Uri uri = new Uri(path);
            if (uri.IsFile)
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] binary = StreamtoByteArray(fs);
                fs.Close();
                return binary;
            }
            else
            {
                return WebToByteArray(path);
            }
        }

        /// <summary>
        /// Converts a byte array to an Image.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        static public Image ByteArrayToImage(Byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image img = Image.FromStream(ms);
                ms.Close();
                return img;
            }
        }

        public static byte[] ImageToByteArray(Image imageToConvert)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, ImageFormat.Jpeg);
                byte[] RetVal = ms.ToArray();
                ms.Close();
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        public static byte[] ReadToEOF(Stream stream)
        {
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// Converts a file from the web to a byte array.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public byte[] WebToByteArray(string url)
        {
            Stream stream = null;
            WebResponse reply = null;

            try
            {
                WebRequest webRequest = (WebRequest)WebRequest.Create(url);
                WebProxy proxyObject = WebProxy.GetDefaultProxy();
                proxyObject.UseDefaultCredentials = true;
                webRequest.Proxy = proxyObject;
                reply = webRequest.GetResponse();
                stream = reply.GetResponseStream();
                byte[] data = ReadToEOF(stream);
                stream.Close();
                reply.Close();
                return data;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (reply != null)
                    reply.Close();
            }
        }

        /// <summary>
        /// Serialize, compress and write the data to the specified file.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <param name="data">Data to save.</param>
        static public void SaveDataToFile<T>(string path, T data)
        {
            FileInfo fInfo = new FileInfo(path);

            if (fInfo.Extension == ".mwr")
            {
                // Serialize data
                MemoryStream ms = new MemoryStream();
                BinaryFormatter f = new BinaryFormatter();
                f.Serialize(ms, data);
                ms.Flush();
                byte[] buffer = ms.ToArray();
                ms.Close();

                // Compress data and write to the file
                FileStream fs = new FileStream(path, FileMode.Create);
                Deflater defl = new Deflater();
                defl.SetLevel(1);
                Stream s = new DeflaterOutputStream(fs, defl);
                s.Write(buffer, 0, buffer.Length);
                s.Flush();
                s.Close();
                fs.Close();
            }
            else if (fInfo.Extension == ".xml")
            {
                // Serialize data
                MemoryStream ms = new MemoryStream();
                System.Xml.Serialization.XmlSerializer f = new System.Xml.Serialization.XmlSerializer(typeof(T));
                f.Serialize(ms, data);
                ms.Flush();
                byte[] buffer = ms.ToArray();
                ms.Close();

                // Write to the file
                FileStream fStream = fInfo.Create();
                fStream.Write(buffer, 0, buffer.Length);
                fStream.Close();
            }
            else
                throw new NotImplementedException("This extension (" + fInfo.Extension + ") hasn't been implemented.");
        }

        /// <summary>
        /// Load, uncompress and unserialize the data from the specified file.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <returns>Generated data.</returns>
        static public T LoadDataFromFile<T>(string path)
        {
            ResolveEventHandler loadComponentAssembly = new ResolveEventHandler(LoadComponentAssembly);
            AppDomain.CurrentDomain.AssemblyResolve += loadComponentAssembly;
            T data = default(T);

            try
            {
                FileInfo fInfo = new FileInfo(path);
                if (fInfo.Extension == ".mwr")
                {
                    byte[] binary = FileToByteArray(path);
                    Stream s = new InflaterInputStream(new MemoryStream(binary));
                    BinaryFormatter f = new BinaryFormatter();
                    f.Binder = new ToVersion16SerializationBinder();
                    data = (T)f.Deserialize(s);
                    s.Close();
                }
                else if (fInfo.Extension == ".xml")
                {
                    System.Xml.Serialization.XmlSerializer f = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    Stream s = fInfo.OpenRead();
                    data = (T)f.Deserialize(s);
                    s.Close();
                }
                else
                    throw new NotImplementedException("This extension (" + fInfo.Extension + ") hasn't been implemented.");

            }
            catch
            {
                throw;
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= loadComponentAssembly;
            }
            return data;
        }

        sealed class ToVersion16SerializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type typeToDeserialize = null;

                // For each assemblyName/typeName that you want to deserialize to
                // a different type, set typeToDeserialize to the desired type.
                String currentAssembly = Assembly.GetExecutingAssembly().FullName;

                if (assemblyName != currentAssembly)
                    assemblyName = currentAssembly;

                // The following line of code returns the type.
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                return typeToDeserialize;
            }
        }

        static Assembly LoadComponentAssembly(Object sender, ResolveEventArgs args)
        {
            Assembly[] loaded = AppDomain.CurrentDomain.GetAssemblies();
            if (args.Name == "Data" || args.Name == "Data, Version=1.2.0.0, Culture=neutral, PublicKeyToken=c90132a2743fc645")
            {
                foreach (Assembly current in loaded)
                    if (current.ManifestModule.Name == "Rincevent.exe")
                        return current;
            }
            string simpleName = args.Name.Substring(0, args.Name.IndexOf(','));
            string fileName = simpleName + ".dll";
            foreach (Assembly current in loaded)
                if (current.ManifestModule.Name == fileName)
                    return current;
            return null;
        }
    }
}
