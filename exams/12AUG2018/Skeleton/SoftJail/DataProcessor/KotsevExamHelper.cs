﻿namespace KotsevHelper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Serialization;

    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;
    using JsonFormatting = Newtonsoft.Json.Formatting;

    public static class KotsevExamHelper
    {
        /// <summary>
        /// Gets an object instance from DbSet T
        /// </summary>
        /// <returns>The object from set.</returns>
        /// <param name="predicate">Predicate.</param>
        /// <param name="context">Context.</param>
        /// <typeparam name="T">Type of object from set</typeparam>
        /// <typeparam name="V">Type of DbContext</typeparam>
        public static T GetObjectFromSet<T, V>(Func<T, bool> predicate, V context)
            where T : class, new()
            where V : DbContext
        {

            //if (context.GetType().BaseType != typeof(DbContext))
            //throw new ArgumentException("")

            var dbSetType = context.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.PropertyType.IsAssignableFrom(typeof(DbSet<T>)));

            if (dbSetType == null)
                throw new ArgumentException($"DbSet with type: {dbSetType.Name} does not exist!");

            DbSet<T> set = (DbSet<T>)dbSetType
                .GetMethod
                .Invoke(context, null);

            T desiredObject = set
                .FirstOrDefault(predicate);

            return desiredObject;
        }

        /// <summary>
        /// Serializes the object(s) to XML.
        /// </summary>
        /// <returns>XML string.</returns>
        /// <param name="values">Object(s) to serialize.</param>
        /// <param name="rootName">Root name.</param>
        /// <param name="omitXmlDeclaration">If set to <c>true</c> omit xml declaration.</param>
        /// <param name="indentXml">If set to <c>true</c> indent xml.</param>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        public static string SerializeObjectToXml<T>(T values, string rootName, bool omitXmlDeclaration = false,
            bool indentXml = true)
        {
            string xml = string.Empty;

            var serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(rootName));

            var settings = new XmlWriterSettings()
            {
                Indent = indentXml,
                OmitXmlDeclaration = omitXmlDeclaration
            };

            XmlSerializerNamespaces @namespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, values, @namespace);
                xml = stream.ToString();
            }

            return xml;
        }


        /// <summary>
        /// Serializes the object(s) to json.
        /// </summary>
        /// <returns>JSON string</returns>
        /// <param name="object">Object to serialize.</param>
        /// <param name="ignoreNullValues">If set to <c>true</c> ignore null values.</param>
        /// <param name="indentJson">If set to <c>true</c> indent json.</param>
        /// <param name="shouldUseCamelCase">If set to <c>true</c> should use camel case.</param>
        public static string SerializeObjectToJson
            (object @object, bool ignoreNullValues = false, bool indentJson = true, bool shouldUseCamelCase = false)
        {

            var serializerSettings = new JsonSerializerSettings();

            if (ignoreNullValues)
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;

            if (indentJson == false)
                serializerSettings.Formatting = JsonFormatting.None;

            if (shouldUseCamelCase)
                serializerSettings.ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() };


            string json = JsonConvert.SerializeObject(@object, serializerSettings);

            return json;
        }

        /// <summary>
        /// Deserializes the object(s) from json.
        /// </summary>
        /// <returns>Object(s) from json.</returns>
        /// <param name="jsonInput">Json input.</param>
        /// <typeparam name="T">Type of desired object</typeparam>
        public static T DeserializeObjectFromJson<T>(string jsonInput, string dateFormatString = "dd/MM/yyyy")
        {
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = dateFormatString;

            var deserializedObject = JsonConvert.DeserializeObject<T>(jsonInput, jsonSettings);

            return deserializedObject;
        }


        public static bool IsValid(object @object, bool validateAllProperties = true)
        {
            ICollection<ValidationResult> validations = new List<ValidationResult>();

            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(@object);

            bool isValid = Validator.TryValidateObject(@object, validationContext, validations, validateAllProperties: validateAllProperties);

            return isValid;
        }


        [Obsolete]
        private static bool IsEntityEqual
            (this PropertyInfo[] properties, string[] propertyNames, object[] values, object obj)
        {
            bool isEqual = true;

            properties = properties.Where(p => propertyNames.Contains(p.Name)).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                if ((string)properties[i].GetValue(obj) != (string)values[i])
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }
    }
}
