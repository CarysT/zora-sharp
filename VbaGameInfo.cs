using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Zyrenth.OracleHack
{

	public class VbaGameInfo
	{
		private short _GameId;
		private string _HeroName;
		private string _KidName;
		private VbaGameType _Version;
		private VbaAnimalType _Animal;
		private Rings _Rings;

		public short GameId
		{
			get { return _GameId; }
			set { _GameId = value; }
		}

		public string HeroName
		{
			get { return _HeroName; }
			set { _HeroName = value; }
		}

		public string KidName
		{
			get { return _KidName; }
			set { _KidName = value; }
		}

		public VbaGameType Version
		{
			get { return _Version; }
			set { _Version = value; }
		}

		public VbaAnimalType Animal
		{
			get { return _Animal; }
			set { _Animal = value; }
		}

		public Rings Rings
		{
			get { return _Rings; }
			set { _Rings = value; }
		}

		public VbaGameInfo()
		{


		}

		public static VbaGameInfo Load(Stream fsSource)
		{
			VbaGameInfo info = new VbaGameInfo();

			fsSource.Seek(19, SeekOrigin.Begin);
			// Read the source file into a byte array.
			byte[] versionBytes = new byte[1];
			int numBytesToRead = 1;
			int numBytesRead = 0;
			while (numBytesToRead > 0)
			{
				// Read may return anything from 0 to numBytesToRead.
				int n = fsSource.Read(versionBytes, numBytesRead, numBytesToRead);

				// Break when the end of the file is reached.
				if (n == 0)
					break;

				numBytesRead += n;
				numBytesToRead -= n;
			}

			info.Version = (VbaGameType)versionBytes[0];


			fsSource.Seek(96, SeekOrigin.Begin);
			// Read the source file into a byte array.
			byte[] gameIdBytes = new byte[2];
			numBytesToRead = 2;
			numBytesRead = 0;
			while (numBytesToRead > 0)
			{
				// Read may return anything from 0 to numBytesToRead.
				int n = fsSource.Read(gameIdBytes, numBytesRead, numBytesToRead);

				// Break when the end of the file is reached.
				if (n == 0)
					break;

				numBytesRead += n;
				numBytesToRead -= n;
			}

			info.GameId = BitConverter.ToInt16(gameIdBytes, 0);

			//fsSource.Seek(98, SeekOrigin.Begin);
			// Read the source file into a byte array.
			byte[] heroBytes = new byte[5];
			numBytesToRead = 5;
			numBytesRead = 0;
			while (numBytesToRead > 0)
			{
				// Read may return anything from 0 to numBytesToRead.
				int n = fsSource.Read(heroBytes, numBytesRead, numBytesToRead);

				// Break when the end of the file is reached.
				if (n == 0)
					break;

				numBytesRead += n;
				numBytesToRead -= n;
			}

			info.HeroName = System.Text.Encoding.ASCII.GetString(heroBytes);


			//fsSource.Seek(105, SeekOrigin.Begin);
			fsSource.Seek(2, SeekOrigin.Current);
			// Read the source file into a byte array.
			byte[] kidBytes = new byte[5];
			numBytesToRead = 5;
			numBytesRead = 0;
			while (numBytesToRead > 0)
			{
				// Read may return anything from 0 to numBytesToRead.
				int n = fsSource.Read(kidBytes, numBytesRead, numBytesToRead);

				// Break when the end of the file is reached.
				if (n == 0)
					break;

				numBytesRead += n;
				numBytesToRead -= n;
			}

			info.KidName = System.Text.Encoding.ASCII.GetString(kidBytes);


			fsSource.Seek(2, SeekOrigin.Current);
			// Read the source file into a byte array.
			byte[] animalBytes = new byte[1];
			numBytesToRead = 1;
			numBytesRead = 0;
			while (numBytesToRead > 0)
			{
				// Read may return anything from 0 to numBytesToRead.
				int n = fsSource.Read(animalBytes, numBytesRead, numBytesToRead);

				// Break when the end of the file is reached.
				if (n == 0)
					break;

				numBytesRead += n;
				numBytesToRead -= n;
			}

			info.Animal = (VbaAnimalType)animalBytes[0];


			fsSource.Seek(5, SeekOrigin.Current);
			//fsSource.Seek(118, SeekOrigin.Begin);
			// Read the source file into a byte array.
			byte[] ringBytes = new byte[8];
			numBytesToRead = 8;
			numBytesRead = 0;
			while (numBytesToRead > 0)
			{
				// Read may return anything from 0 to numBytesToRead.
				int n = fsSource.Read(ringBytes, numBytesRead, numBytesToRead);

				// Break when the end of the file is reached.
				if (n == 0)
					break;

				numBytesRead += n;
				numBytesToRead -= n;
			}
			ulong rings = BitConverter.ToUInt64(ringBytes, 0);
			info.Rings = (Rings)rings;

			return info;
		}
	}
}
