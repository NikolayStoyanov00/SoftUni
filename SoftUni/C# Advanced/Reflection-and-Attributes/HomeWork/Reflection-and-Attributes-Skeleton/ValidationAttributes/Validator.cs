﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace ValidationAttributes
{
    public class Validator
    {
        public static bool IsValid(object obj)
        {
            var objProperties = obj.GetType()
                .GetProperties();

            foreach (var propInfo in objProperties)
            {
                var propAttributes = propInfo
                    .GetCustomAttributes()
                    .Where(a => a is MyValidationAttribute)
                    .Cast<MyValidationAttribute>();

                foreach (var attrtibute in propAttributes)
                {
                    bool result = attrtibute.IsValid(propInfo.GetValue(obj));

                    if (!result)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
