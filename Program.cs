using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace AssemblyTester
{
	class Program
	{
		static int Main(string[] args)
		{
			if(args.Count() == 0)
			{
				Console.Error.WriteLine("Empty command-line.");
				return 0;
			}

			int classCount = 0;

			try
			{
				Assembly asm = Assembly.LoadFile(Path.GetFullPath(args[0]));

				IEnumerable<TypeInfo> types = asm.DefinedTypes;

				foreach(TypeInfo ti in types)
				{
					if(ti.IsClass)
					{
						Console.Error.WriteLine("Found class '{0}'.", ti.Name);
						classCount++;
					}
				}

				/*
				classCount = Assembly.LoadFile(Path.GetFullPath(args[0])).DefinedTypes.Select(ti => ti.IsClass).Count();
				*/
			}
			catch(BadImageFormatException)
			{
				Console.Error.WriteLine("Assembly '{0}' is not loadable.", args[0]);
			}
			catch(Exception e)
			{
				Console.Error.WriteLine("Assembly '{0}' throws exception {1}.", args[0], e.Message);
			}

			Console.WriteLine("Assembly '{1}', Number of classes: {0}", classCount, args[0]);

			return classCount;
		}
	}
}
