namespace TrieDictionaryTest;

[TestClass]
public class TrieTest
{
    // Test that a word is inserted in the trie
    [TestMethod]
    public void InsertWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";

        // Act
        bool result = trie.Insert(word);

        // Assert
        Assert.IsTrue(result);
    }

    // Test that a word is deleted from the trie
    [TestMethod]
    public void DeleteWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Act
        trie.Delete(word);

        // Assert
        Assert.IsFalse(trie.Search(word));
    }

    // Test that a word is not inserted twice in the trie
    [TestMethod]
    public void InsertDuplicateWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Act
        bool result = trie.Insert(word);

        // Assert
        Assert.IsFalse(result);
    }

    // Test that a word is deleted from the trie
    [TestMethod]
    public void DeleteNonExistentWord()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";

        // Act
        trie.Delete(word);

        // Assert
        Assert.IsFalse(trie.Search(word));
    }

    // Test that a word is not deleted from the trie if it is not present
    [TestMethod]
    public void DeleteWordNotInTrie()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        trie.Insert(word);

        // Act
        trie.Delete("world");

        // Assert
        Assert.IsTrue(trie.Search(word));
    }

    // Test that a word is deleted from the trie if it is a prefix of another word
    [TestMethod]
    public void DeleteWordWithPrefix()
    {
        // Arrange
        Trie trie = new Trie();
        string word = "hello";
        string prefix = "hell";
        trie.Insert(word);

        // Act
        trie.Delete(prefix);

        // Assert
        Assert.IsFalse(trie.Search(word));
    }

    // Test AutoSuggest for the prefix "cat" not present in the 
    // trie containing "catastrophe", "catatonic", and "caterpillar"
    [TestMethod]
    public void AutoSuggestPrefixNotPresent()
    {
        // Arrange
        Trie trie = new Trie();
        trie.Insert("catastrophe");
        trie.Insert("catatonic");
        trie.Insert("caterpillar");

        // Act
        List<string> suggestions = trie.AutoSuggest("cat");

        // Assert
        CollectionAssert.AreEqual(new List<string>(), suggestions);
    }

    // Test AutoSuggest for the prefix "cat" present in the
    // trie containing "catastrophe", "catatonic", and "caterpillar"
    [TestMethod]
    public void AutoSuggestPrefixPresent()
    {
        // Arrange
        Trie trie = new Trie();
        trie.Insert("catastrophe");
        trie.Insert("catatonic");
        trie.Insert("caterpillar");

        // Act
        List<string> suggestions = trie.AutoSuggest("cat");

        // Assert
        CollectionAssert.AreEqual(new List<string> { "catastrophe", "catatonic", "caterpillar" }, suggestions);
    }

    // Test GetSpellingSuggestions for a word not present in the trie
    [TestMethod]
    public void GetSpellingSuggestionsWordNotPresent()
    {
        // Arrange
        Trie trie = new Trie();
        trie.Insert("catastrophe");
        trie.Insert("catatonic");
        trie.Insert("caterpillar");

        // Act
        List<string> suggestions = trie.GetSpellingSuggestions("catatonicc");

        // Assert
        CollectionAssert.AreEqual(new List<string> { "catatonic" }, suggestions);
    }



    
}