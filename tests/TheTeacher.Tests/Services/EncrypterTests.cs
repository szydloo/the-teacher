using FluentAssertions;
using NUnit.Framework;
using TheTeacher.Infrastructure.Services;

namespace TheTeacher.Tests.Services
{
    public class EncrypterTests
    {
        IEncrypter _encrypter;

        [SetUp]
        public void Setup()
        {
            _encrypter = new Encrypter();
        }

        [Test]
        public void hashing_string_should_return_hashed()
        {
            var pass1 = "secret";
            var salt1 = _encrypter.GetSalt();

            var hash1 = _encrypter.GetHash(pass1,salt1);
            hash1.Should().BeOfType<string>();
            hash1.Should().NotBe(pass1);
            hash1.Should().NotBe(salt1);
            
        }
        [Test]
        public void result_of_hashing_two_diferent_strings_should_not_be_equal()
        {
            var pass1 = "secret";
            var pass2 = "secret2";
            var salt1 = _encrypter.GetSalt();
            var salt2 = _encrypter.GetSalt();

            var hash1 = _encrypter.GetHash(pass1,salt1);
            var hash2 = _encrypter.GetHash(pass2,salt2);

            hash1.Should().NotBe(hash2);
        }

        [Test]
        public void same_passwrds_should_return_same_hashes()
        {
            var pass1 = "secret";
            var pass2 = "secret";
            var salt1 = _encrypter.GetSalt();

            var hash1 = _encrypter.GetHash(pass1,salt1);
            var hash2 = _encrypter.GetHash(pass2,salt1);

            hash1.ShouldBeEquivalentTo(hash2);
        }
    }
}