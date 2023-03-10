---
uid: Lucene.Net.Facet.Taxonomy
summary: *content
---

<!--
 Licensed to the Apache Software Foundation (ASF) under one or more
 contributor license agreements.  See the NOTICE file distributed with
 this work for additional information regarding copyright ownership.
 The ASF licenses this file to You under the Apache License, Version 2.0
 (the "License"); you may not use this file except in compliance with
 the License.  You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
-->

# Taxonomy of Categories

	Facets are defined using a hierarchy of categories, known as a _Taxonomy_.
	For example, the taxonomy of a book store application might have the following structure:

*   Author

    *   Mark Twain

    *   J. K. Rowling

*   Date

    *   2010

    *   March

    *   April

    *   2009

	The _Taxonomy_ translates category-paths into interger identifiers (often termed _ordinals_) and vice versa.
	The category `Author/Mark Twain` adds two nodes to the taxonomy: `Author` and 
	`Author/Mark Twain`, each is assigned a different ordinal. The taxonomy maintains the invariant that a 
	node always has an ordinal that is < all its children.