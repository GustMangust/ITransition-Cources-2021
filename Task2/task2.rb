require 'digest'
Digest::SHA3.new(256).hexdigest('')
for value in Dir[Dir.pwd+"/*"] do
    #data = Filopenssl list -digest-algorithmse.binread(value)
    sha256 = OpenSSL::Digest.new('SHA256')
    #digest = sha256.digest(data)
    puts value[value.rindex("/")+1..value.rindex(".")-1]+" "+digest
end