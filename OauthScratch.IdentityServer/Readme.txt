Can run InstallCert.bat

for cert details see readme here:
https://github.com/thinktecture/Thinktecture.IdentityServer.v3.Samples/tree/master/source/Certificates

	How to install the test certificates
	The TestOptionsFactory uses an embedded certificate for token signing and validation. So there are no additional steps necessary. On machines where you want to consume tokens, you only need to install the idsrv3test.cer into the local computer/trusted people store (see step 3).

	If you want to load certificates from the certificate store, we provide the idsrv3test public/private key pair for testing (including the root certificate). Use the following instructions:

	Open mmc.exe and add the certificates snap-in. Use the local computer store.

	Install idsrv3test.pfx into your local computer/personal store (password is: idsrv3test)
	Right click -> All tasks -> Manage private key on the imported certificate and give your account read access
	Install idsrv3test.cer into your local computer/trusted people store

---------------------------------------------------