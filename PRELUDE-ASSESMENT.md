Certificate Authority (CA)
A Certificate Authority (CA) is a trusted organization or entity responsible for issuing, managing, and revoking digital certificates. The CA verifies the identity of the certificate requestor before signing and issuing the certificate.

Chain of Trust
The Chain of Trust is a hierarchical structure of certificates linked together to ensure trustworthiness. It starts with a Root CA, followed by Intermediate CAs, and ends with an End-Entity Certificate. Each certificate in the chain is signed by the entity above it, proving its legitimacy.

Certificate Revocation List (CRL)
A Certificate Revocation List (CRL) is a list of revoked digital certificates maintained by a Certificate Authority (CA). It allows systems to check whether a certificate has been invalidated before trusting it.

 X.509 Certificate
An X.509 Certificate is a standardized format for public key certificates used in Public Key Infrastructure (PKI). It contains details such as the subject, issuer, validity period, public key, and digital signature of the certificate.

Certificate Signing Request (CSR)
A Certificate Signing Request (CSR) is a digitally signed message sent to a Certificate Authority (CA) requesting the issuance of a certificate. It includes the public key, identity details, and a digital signature generated using the requestor's private key.

RSA Key Pair-is a asymmetric encryption algorithm that consists of two cryptographic keys:

Private Key – Used for signing and decrypting data.
Public Key – Used for verifying signatures and encrypting data.

Root CA
A Root CA (Root Certificate Authority) is the topmost entity in the Chain of Trust. It issues certificates to Intermediate CAs and End-Entity Certificates. The Root CA’s certificate is self-signed and trusted implicitly by operating systems and browsers.

End-Entity Certificate
An End-Entity Certificate is the final certificate in the Chain of Trust, assigned to users, servers, or devices. It is issued by a CA and used for purposes like authentication, encryption, and digital signatures.