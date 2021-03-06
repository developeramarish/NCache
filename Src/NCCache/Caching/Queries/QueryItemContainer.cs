﻿// Copyright (c) 2018 Alachisoft
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections;

namespace Alachisoft.NCache.Caching.Queries
{
    public class QueryItemContainer
    {
        private CacheEntry _item;

        public CacheEntry Item
        {
            get { return _item; }
            set { _item = value; }
        }
        private Hashtable _itemArrtribs;

        public Hashtable ItemArrtributes
        {
            get { return _itemArrtribs; }
            set { _itemArrtribs = value; }
        }

        public QueryItemContainer(CacheEntry item, Hashtable itemAttributes)
        {
            _item = item;
            _itemArrtribs = itemAttributes;
        }
    }
}
