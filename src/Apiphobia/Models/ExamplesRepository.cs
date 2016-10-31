using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Apiphobia.Models
{
    public class ExamplesRepository
    {
        private readonly ConcurrentDictionary<string, Example> _examples = new ConcurrentDictionary<string, Example>();

        public void Add(Example example)
        {
            example.Id = Guid.NewGuid().ToString();
            _examples[example.Id] = example;
        }

        public Example FindOrDefault(string id)
        {
            Example example;
            return _examples.TryGetValue(id, out example) ? example : default(Example);
        }

        public IEnumerable<Example> FindAll()
        {
            return _examples.Select(r => r.Value);
        }

        public void Remove(string id)
        {
            Example example;
            _examples.TryRemove(id, out example);
        }
    }
}