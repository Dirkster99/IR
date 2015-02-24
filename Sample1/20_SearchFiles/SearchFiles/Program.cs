namespace SearchFiles
{
	using System;
	using System.IO;
	using System.Reflection;
	using Lucene.Net.Analysis;
	using Lucene.Net.Analysis.Standard;
	using Lucene.Net.QueryParsers;
	using Lucene.Net.Search;
	using Lucene.Net.Store;

	class Program
	{
		[STAThread]
		public static void Main(System.String[] args)
		{
			try
			{
				string idxPathDir = string.Empty;

				System.String usage = Assembly.GetEntryAssembly().FullName + " <index_directory>";
				if (args.Length < 1)
				{
					System.Console.Error.WriteLine("Usage: " + usage);
					System.Environment.Exit(1);

					System.Console.Out.WriteLine("Press any key...");
					System.Console.ReadKey();
				}
				else
					idxPathDir = args[0];

				string BasePAth = System.IO.Directory.GetParent(Assembly.GetEntryAssembly().FullName).FullName;
				idxPathDir = Path.GetFullPath(Path.Combine(BasePAth, idxPathDir));

				// Check whether the "index" directory exists.
				// Exit program, If not.
				if (System.IO.Directory.Exists(idxPathDir) == false)
				{
					System.Console.Out.WriteLine("Cannot load index from '" + idxPathDir + "' directory, please make sure it exists.");

					System.Console.WriteLine("Press any key...");
					System.Console.ReadKey();

					System.Environment.Exit(1);
				}

				Searcher searcher = new IndexSearcher(FSDirectory.Open(new DirectoryInfo(idxPathDir)));
				Analyzer analyzer = new StandardAnalyzer(new Lucene.Net.Util.Version());


				// Create a new StreamReader using standard input as the stream
				System.IO.StreamReader streamReader =
						new System.IO.StreamReader(
					// Sets reader's input stream to the standard input stream
								new System.IO.StreamReader(
										System.Console.OpenStandardInput(),
										System.Text.Encoding.Default).BaseStream,
					// Sets reader's encoding to whatever standard input is using
								new System.IO.StreamReader(
										System.Console.OpenStandardInput(),
										System.Text.Encoding.Default).CurrentEncoding);
				while (true)
				{
					System.Console.Out.Write("Query: ");
					string line = streamReader.ReadLine();

					if (line.Length <= 0)
						break;

					QueryParser parser = new QueryParser(new Lucene.Net.Util.Version(), line, analyzer);

					Query query = parser.Parse(line);
					System.Console.Out.WriteLine("Searching for: " + query.ToString("contents"));

					// http://stackoverflow.com/questions/14966208/hits-object-deprecated-in-lucene-net-3-03-how-do-i-replace-it
					Lucene.Net.Search.TopDocs results = searcher.Search(query, 100);
					System.Console.Out.WriteLine(results.TotalHits + " total matching documents");

					foreach (ScoreDoc scoreDoc in results.ScoreDocs)
					{
						// retrieve the document from the 'ScoreDoc' object
						Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
						string docPath = doc.Get("path");

						System.Console.Out.WriteLine(docPath);
					}
				}
				searcher.Dispose();

			}
			catch (System.Exception e)
			{
				System.Console.Out.WriteLine(" caught a " + e.GetType() + "\n with message: " + e.Message);
			}
		}
	}
}
