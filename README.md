# IR GitHub Repository
The IR project is a collection of .Net sample applications based on Lucene .Net version 3.

## Sample1 Sub-Folder

This folder contains 2 console applications to demo indexing and searching files
in the file system based based on Lucene.Net.

The index builder Sample1\10_IndexBuildTest creates the index only if there is not already one present.

The index searcher searches the build index in Sample1\00_IDX sub-directory. See the 
\Sample1\20_SearchFiles\SearchIndex.bat

for usage and query samples.

The fields that can be queried are defined here:

10_IndexBuildTest\IndexBuildTest\Program.cs
See IndexDocument() method.

See also \Sample1\01_Documentation sub-folder for more documentation details about Lucene index creation and search in .Net.