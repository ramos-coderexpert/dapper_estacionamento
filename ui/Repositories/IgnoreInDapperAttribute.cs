﻿namespace Dapper_estacionamento.Repositories
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class IgnoreInDapperAttribute : Attribute
    {
        public IgnoreInDapperAttribute() { }
    }
}
