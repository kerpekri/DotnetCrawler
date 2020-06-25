using DotnetCrawler.Data.Attributes;
using DotnetCrawler.Data.Repository;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCrawler.Processor
{
    public class DotnetCrawlerProcessor<TEntity> : IDotnetCrawlerProcessor<TEntity> where TEntity : class, IEntity
    {
        public async Task<IEnumerable<TEntity>> Process(HtmlDocument document)
        {
            var nameValueDictionary = GetColumnNameValuePairsFromHtml(document);

            var processorEntity = ReflectionHelper.CreateNewEntity<TEntity>();
            foreach (var pair in nameValueDictionary)
            {
                ReflectionHelper.TrySetProperty(processorEntity, pair.Key, pair.Value);
            }

            return new List<TEntity>
            {
                processorEntity as TEntity
            };
        }

        private static Dictionary<string, object> GetColumnNameValuePairsFromHtml(HtmlDocument document)
        {
            var columnNameValueDictionary = new Dictionary<string, object>();

            var entityExpression = ReflectionHelper.GetEntityExpression<TEntity>();
            var propertyExpressions = ReflectionHelper.GetPropertyAttributes<TEntity>();

            // todo: kke: handle that this can be null!! and log this ASAP!
            HtmlNode entityNode = document.DocumentNode.SelectSingleNode(entityExpression);
            // todo: kke: handle that this can be null!! and log this ASAP!

            ProcessProperties(columnNameValueDictionary, propertyExpressions, entityNode);

            return columnNameValueDictionary;
        }

        private static void ProcessProperties(Dictionary<string, object> columnNameValueDictionary, Dictionary<string, Tuple<SelectorType, string>> propertyExpressions, HtmlNode entityNode)
        {
            foreach (var expression in propertyExpressions)
            {
                var columnName = expression.Key;
                object columnValue = null;
                var fieldExpression = expression.Value.Item2;

                switch (expression.Value.Item1)
                {
                    case SelectorType.XPath:
                        var node = entityNode.SelectSingleNode(fieldExpression);
                        if (node != null)
                            columnValue = node.InnerText;
                        break;
                    case SelectorType.CssSelector:
                        var nodeCss = entityNode.QuerySelector(fieldExpression);
                        if (nodeCss != null)
                            columnValue = nodeCss.InnerText;
                        break;
                    case SelectorType.FixedValue:
                        if (Int32.TryParse(fieldExpression, out var result))
                        {
                            columnValue = result;
                        }
                        break;
                    default:
                        break;
                }
                columnNameValueDictionary.Add(columnName, columnValue);
            }
        }
    }
}
