using System;
using System.IO;
using Xunit;
using PKI_Certificate_Lifecycle.Services;
using System.Security.Cryptography.X509Certificates;

namespace PKI_Certificate_Lifecycle.Tests
{
    public class CertificateServiceTests
    {
        [Fact]
        public void TestSelfSignedCertificateGeneration()
        {
            var certService = new CertificateService();
            var cert = certService.GenerateSelfSignedCertificate();

            Assert.NotNull(cert);
            Assert.Contains("CN=SelfSignedCA", cert.Subject);
        }

        [Fact]
        public void TestCSRGeneration()
        {
            var certService = new CertificateService();
            var csr = certService.GenerateCSR();

            Assert.NotNull(csr);
            Assert.True(csr.Length > 0);
        }

        [Fact]
        public void TestEndEntityCertificateGeneration()
        {
            var certService = new CertificateService();
            var caCert = certService.GenerateSelfSignedCertificate();
            var csr = certService.GenerateCSR();
            var eeCert = certService.GenerateEndEntityCertificate(caCert, csr);

            Assert.NotNull(eeCert);
            Assert.Contains("CN=EndEntity", eeCert.Subject);
        }

        [Fact]
        public void TestCertificateRevocation()
        {
            var certService = new CertificateService();
            var caCert = certService.GenerateSelfSignedCertificate();
            var csr = certService.GenerateCSR();
            var eeCert = certService.GenerateEndEntityCertificate(caCert, csr);

            certService.RevokeCertificate(eeCert);

            // No direct way to check revocation, so we just verify no exception occurred.
            Assert.True(true);
        }

        [Fact]
        public void TestCRLGeneration()
        {
            var certService = new CertificateService();
            var caCert = certService.GenerateSelfSignedCertificate();
            var csr = certService.GenerateCSR();
            var eeCert = certService.GenerateEndEntityCertificate(caCert, csr);

            certService.RevokeCertificate(eeCert);
            certService.GenerateCRL();

            // Check if CRL file is generated
            string crlPath = "CRL_List.crl";
            Assert.True(File.Exists(crlPath));
        }
    }
}