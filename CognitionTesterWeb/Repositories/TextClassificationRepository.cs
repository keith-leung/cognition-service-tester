using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CognitionTesterWeb.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CognitionTesterWeb.Repositories
{
    public class TextClassificationRepository : IQueryable<TextClassificationResult>
    {
        public TextClassificationRepository()
        {
            mongoConnection = new MongoConnection();
            this.collection = mongoConnection.GetCollection<TextClassificationResult>(
                "textclassification_result");
        }

        private MongoConnection mongoConnection;
        private IMongoCollection<TextClassificationResult> collection;

        public IEnumerator<TextClassificationResult> GetEnumerator()
        {
            return collection.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => typeof(TextClassificationResult);

        public Expression Expression => collection.AsQueryable().Expression;

        public IQueryProvider Provider => collection.AsQueryable().Provider;

        public IQueryable<TextClassificationResult> Results => collection.AsQueryable();

        public void Add(TextClassificationResult newResult)
        {
            collection.InsertOne(newResult);
        }
    }
}
