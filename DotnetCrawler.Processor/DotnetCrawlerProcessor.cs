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

            HtmlNode entityNode = document.DocumentNode.SelectSingleNode(entityExpression);

            if (entityNode != null)
            {
                ProcessProperties(columnNameValueDictionary, propertyExpressions, entityNode);
            }

            return columnNameValueDictionary;
        }

        private static void ProcessProperties(Dictionary<string, object> columnNameValueDictionary, Dictionary<string, Tuple<SelectorType, string>> propertyExpressions, HtmlNode entityNode)
        {
            foreach (var expression in propertyExpressions)
            {
                var columnName = expression.Key;
                object columnValue = null;
                var fieldExpression = expression.Value.Item2;

                columnValue = SelectorType(entityNode, expression, columnValue, fieldExpression);
                columnNameValueDictionary.Add(columnName, columnValue);
            }
        }

        private static object SelectorType(HtmlNode entityNode, KeyValuePair<string, Tuple<SelectorType, string>> expression, object columnValue, string fieldExpression)
        {
            switch (expression.Value.Item1)
            {
                case Data.Attributes.SelectorType.XPath:
                    var node = entityNode.SelectSingleNode(fieldExpression);
                    if (node != null)
                        columnValue = node.InnerText;
                    break;
                case Data.Attributes.SelectorType.CssSelector:
                    var nodeCss = entityNode.QuerySelector(fieldExpression);
                    if (nodeCss != null)
                        columnValue = nodeCss.InnerText;
                    break;
                case Data.Attributes.SelectorType.FixedValue:
                    if (int.TryParse(fieldExpression, out var result))
                    {
                        columnValue = result;
                    }
                    break;
                default:
                    break;
            }

            return columnValue;
        }
    }
}
