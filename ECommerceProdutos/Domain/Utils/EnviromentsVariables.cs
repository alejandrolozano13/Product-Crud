﻿namespace Domain.Utils
{
    public static class EnviromentsVariables
    {
        public static string Jwt_Key = Environment.GetEnvironmentVariable("JWT_KEY")
            ?? throw new Exception("Chave JWT não configurada");

        public static string Jwt_Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
           ?? throw new Exception("Issuer JWT não configurado");

        public static string Jwt_Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
            ?? throw new Exception("Audience JWT não configurado");
    }
}