CertUtil -addstore -f -v root idsrv3test.cer

CertUtil -f -p "idsrv3test" -importpfx idsrv3test.pfx