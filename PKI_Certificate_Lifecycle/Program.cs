using System;
using PKI_Certificate_Lifecycle.Services;

class Program
{
    static void Main()
    {
        Console.WriteLine("PKI Certificate Lifecycle Management");
        var certService = new CertificateService();

        // Generate Self-Signed CA Certificate
        var caCertificate = certService.GenerateSelfSignedCertificate();

        // Generate CSR for EE Certificate
        var csr = certService.GenerateCSR();
        
        // Issue EE Certificate using CSR
        var eeCertificate = certService.GenerateEndEntityCertificate(caCertificate, csr);

        // Revoke EE Certificate
        certService.RevokeCertificate(eeCertificate);

        // Generate CRL File
        certService.GenerateCRL();

        Console.WriteLine("Certificate Lifecycle Completed Successfully!");
    }
}