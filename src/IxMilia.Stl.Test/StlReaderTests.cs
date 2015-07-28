﻿// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.IO;
using Xunit;

namespace IxMilia.Stl.Test
{
    public class StlReaderTests
    {
        [Fact]
        public void ReadOneVertexTest()
        {
            var file = FromString(@"
solid foo
  facet normal 1 2 3
    outer loop
      vertex 4 5 6
      vertex 7 8 9
      vertex 10 11 12
    endloop
  endfacet
endsolid foo
");
            Assert.Equal("foo", file.SolidName);
            Assert.Equal(1, file.Triangles.Count);
            Assert.Equal(new StlNormal(1, 2, 3), file.Triangles[0].Normal);
            Assert.Equal(new StlVertex(4, 5, 6), file.Triangles[0].Vertex1);
            Assert.Equal(new StlVertex(7, 8, 9), file.Triangles[0].Vertex2);
            Assert.Equal(new StlVertex(10, 11, 12), file.Triangles[0].Vertex3);
        }

        [Fact]
        public void ReadTwoVerticiesTest()
        {
            var file = FromString(@"
solid foo
  facet normal 1 2 3
    outer loop
      vertex 4 5 6
      vertex 7 8 9
      vertex 10 11 12
    endloop
  endfacet
  facet normal 13 14 15
    outer loop
      vertex 16 17 18
      vertex 19 20 21
      vertex 22 23 24
    endloop
  endfacet
endsolid foo
");
            Assert.Equal("foo", file.SolidName);
            Assert.Equal(2, file.Triangles.Count);
            Assert.Equal(new StlNormal(1, 2, 3), file.Triangles[0].Normal);
            Assert.Equal(new StlVertex(4, 5, 6), file.Triangles[0].Vertex1);
            Assert.Equal(new StlVertex(7, 8, 9), file.Triangles[0].Vertex2);
            Assert.Equal(new StlVertex(10, 11, 12), file.Triangles[0].Vertex3);
            Assert.Equal(new StlNormal(13, 14, 15), file.Triangles[1].Normal);
            Assert.Equal(new StlVertex(16, 17, 18), file.Triangles[1].Vertex1);
            Assert.Equal(new StlVertex(19, 20, 21), file.Triangles[1].Vertex2);
            Assert.Equal(new StlVertex(22, 23, 24), file.Triangles[1].Vertex3);
        }

        private StlFile FromString(string content)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content.Trim());
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return StlFile.Load(stream);
        }
    }
}
