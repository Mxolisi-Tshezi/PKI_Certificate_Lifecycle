# PKI Certificate Lifecycle Management (Full Version)
This project demonstrates the complete lifecycle management of certificates using C# and .NET Core.

## Features Implemented
- ✅ RSA Key Pair Generation
- ✅ Self-Signed Certificate Issuance
- ✅ CSR (Certificate Signing Request) Generation
- ✅ End-Entity (EE) Certificate Issuance using CSR
- ✅ Certificate Revocation & CRL File Output
- ✅ CRL Distribution Point (CDP) Extension
- ✅ Key Usage Extensions (Digital Signature, Non-Repudiation)

## Prerequisites
- .NET 6.0+
- Visual Studio 2022
- NuGet Package: `BouncyCastle.NetCore`

## Running the Project
```sh
cd PKI_Certificate_Lifecycle
dotnet run
```

## Running Tests
```sh
cd PKI_Certificate_Lifecycle.Tests
dotnet test
```

## Future Enhancements
- Implement OCSP for real-time certificate validation.
