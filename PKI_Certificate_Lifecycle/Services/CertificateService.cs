using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.X509;
using System.Collections.Generic;

namespace PKI_Certificate_Lifecycle.Services
{
    public class CertificateService
    {
        private List<X509Certificate2> revokedCertificates = new List<X509Certificate2>();

        public X509Certificate2 GenerateSelfSignedCertificate()
        {
            Console.WriteLine("Generating Self-Signed Certificate...");
            var rsaKey = RSA.Create(2048);

            var certRequest = new CertificateRequest(
                "CN=SelfSignedCA",
                rsaKey,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            // Add Basic Constraints to mark as CA
            certRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));

            // Allow the CA to Sign Other Certificates
            certRequest.CertificateExtensions.Add(new X509KeyUsageExtension(
                X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.DigitalSignature, true));

            // CRL Distribution Point Extension (Placeholder)
            certRequest.CertificateExtensions.Add(new X509Extension("2.5.29.31", new byte[] { 0x30, 0x1F, 0x86, 0x1D, 0x68, 0x74, 0x74, 0x70,
                0x3A, 0x2F, 0x2F, 0x63, 0x61, 0x2E, 0x65, 0x78, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x2E, 0x63, 0x6F, 0x6D, 0x2F, 0x63, 0x72, 0x6C },
                false));

            var cert = certRequest.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(10));

            Console.WriteLine("Self-Signed Certificate Generated Successfully!");
            return cert;
        }

        public byte[] GenerateCSR()
        {
            Console.WriteLine("Generating CSR for End-Entity...");
            var rsaKey = RSA.Create(2048);
            var csrRequest = new CertificateRequest(
                "CN=EndEntity",
                rsaKey,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            return csrRequest.CreateSigningRequest();
        }

        public X509Certificate2 GenerateEndEntityCertificate(X509Certificate2 caCert, byte[] csr)
        {
            Console.WriteLine("Issuing End-Entity Certificate...");
            var rsaKey = RSA.Create(2048);
            var certRequest = new CertificateRequest(
                "CN=EndEntity",
                rsaKey,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            certRequest.CertificateExtensions.Add(new X509KeyUsageExtension(
                X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation, true));

            var eeCert = certRequest.Create(caCert, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(5), Guid.NewGuid().ToByteArray());

            Console.WriteLine("End-Entity Certificate Issued Successfully!");
            return eeCert;
        }

        public void RevokeCertificate(X509Certificate2 cert)
        {
            Console.WriteLine($"Revoking Certificate: {cert.Subject}");
            revokedCertificates.Add(cert);
            Console.WriteLine("Certificate Revoked and Added to CRL.");
        }

        public void GenerateCRL()
        {
            Console.WriteLine("Generating CRL...");

            string crlPath = "CRL_List.crl";
            using (StreamWriter sw = new StreamWriter(crlPath))
            {
                foreach (var cert in revokedCertificates)
                {
                    sw.WriteLine($"Revoked Certificate: {cert.Subject}");
                }
            }

            Console.WriteLine($"CRL Generated Successfully! Saved at {crlPath}");
        }
    }
}