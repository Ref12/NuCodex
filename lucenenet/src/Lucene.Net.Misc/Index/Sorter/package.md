---
uid: Lucene.Net.Index.Sorter
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

Provides index sorting capablities. The application can use any
Sort specification, e.g. to sort by fields using DocValues or FieldCache, or to
reverse the order of the documents (by using SortField.Type.DOC in reverse).
Multi-level sorts can be specified the same way you would when searching, by
building Sort from multiple SortFields.

<xref:Lucene.Net.Index.Sorter.SortingMergePolicy> can be used to
make Lucene sort segments before merging them. This will ensure that every
segment resulting from a merge will be sorted according to the provided
<xref:Lucene.Net.Search.Sort>. This however makes merging and
thus indexing slower.

Sorted segments allow for early query termination when the sort order
matches index order. This makes query execution faster since not all documents
need to be visited. Please note that this is an expert feature and should not
be used without a deep understanding of Lucene merging and document collection.