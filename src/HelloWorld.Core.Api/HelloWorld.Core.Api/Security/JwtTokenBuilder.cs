﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace HelloWorld.Core.Api.Security
{
    public class JwtTokenBuilder
    {
        private SecurityKey _securityKey;
        private string _subject = string.Empty;
        private string _issuer = string.Empty;
        private string _audience = string.Empty;
        private readonly Dictionary<string, string> _claims = new Dictionary<string, string>();
        private int _expiryInDay = 1;

        public JwtTokenBuilder AddSecurityKey(SecurityKey securityKey)
        {
            _securityKey = securityKey;
            return this;
        }

        public JwtTokenBuilder AddSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public JwtTokenBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }

        public JwtTokenBuilder AddAudience(string audience)
        {
            _audience = audience;
            return this;
        }

        public JwtTokenBuilder AddClaim(string type, string value)
        {
            _claims.Add(type, value);
            return this;
        }

        public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
        {
            _claims.Union(claims);
            return this;
        }

        public JwtTokenBuilder AddExpiry(int expiryInDay)
        {
            _expiryInDay = expiryInDay;
            return this;
        }

        public JwtToken Build()
        {
            EnsureArguments();


            ClaimsIdentity claimsIdentity = new ClaimsIdentity();

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Union(_claims.Select(item => new Claim(item.Key, item.Value)));

            claimsIdentity.AddClaims(claims);

            var token = new JwtSecurityToken(
                                       _issuer,
                                       _audience,
                                        claimsIdentity.Claims,
                                        expires: DateTime.UtcNow.AddDays(_expiryInDay),
                                        signingCredentials: new SigningCredentials(
                                            _securityKey,
                                            SecurityAlgorithms.HmacSha256));

            return new JwtToken(token);
        }


        private void EnsureArguments()
        {
            if (_securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(_subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(_issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(_audience))
                throw new ArgumentNullException("Audience");
        }

    }
}