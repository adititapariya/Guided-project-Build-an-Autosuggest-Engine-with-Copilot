public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; set; }
    public bool IsEndOfWord { get; set; }

    public char _value;

    public TrieNode(char value = ' ')
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
        _value = value;
    }

    public bool HasChild(char c)
    {
        return Children.ContainsKey(c);
    }
}

public class Trie
{
    private TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    public bool Insert(string word)
    {
        TrieNode current = root;
        // for each character in the word
        foreach (char c in word)
        {
            if (!current.HasChild(c))
            {
                // if the character is not in the trie, add it
                current.Children[c] = new TrieNode(c);
            }
            current = current.Children[c];
        }
        //if the word is already in the trie
        if (current.IsEndOfWord)
        {
            return false;
        }
        current.IsEndOfWord = true;
        return true;
    }
    
    // Search for a word in the trie
    public bool Search(string word) 
    { 
        TrieNode current = root;
        foreach (char c in word)
        {
            if (!current.HasChild(c))
            {
                return false;
            }
            current = current.Children[c];
        }
        return current.IsEndOfWord;
    }
    /// <summary>
    /// Retrieves a list of suggested words based on the given prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>A list of suggested words.</returns>
    public List<string> AutoSuggest(string prefix)
    {
        TrieNode currentNode = root;
        foreach (char c in prefix)
        {
            if (!currentNode.HasChild(c))
            {
                return new List<string>();
            }
            currentNode = currentNode.Children[c];
        }
        return GetAllWordsWithPrefix(currentNode, prefix);
    }

    private List<string> GetAllWordsWithPrefix(TrieNode root, string prefix)
    {
        List<string> words = new List<string>();
        if (root == null)
        {
            return words;
        }
        if (root.IsEndOfWord)
        {
            words.Add(prefix);
        }
        foreach (var child in root.Children)
        {
            words.AddRange(GetAllWordsWithPrefix(child.Value, prefix + child.Key));
        }
        return words;
    }

    // Delete a word from the trie
    // public bool Delete(string word)
    // {
    //     TrieNode current = root;
    //     foreach (char c in word)
    //     {
    //         if (!current.HasChild(c))
    //         {
    //             // Word doesn't exist in trie
    //             return false;
    //         }
    //         current = current.Children[c];
    //     }
    //     if (!current.IsEndOfWord)
    //     {
    //         // Word doesn't exist in trie
    //         return false;
    //     }
    //     // Word exists in trie
    //     // Set IsEndOfWord to false
    //     current.IsEndOfWord = false;
    //     return true;
    // }  

    // Helper method to delete a word from the trie by recursively removing its nodes
    private bool _delete(TrieNode current, string word, int index)
    {
        // Base case: If the current node is null, the word doesn't exist in the trie
        if (current == null)
        {
            return false;
        }

        // Base case: If all characters of the word have been processed
        if (index == word.Length)
        {
            // If the current node is not the end of a word, the word doesn't exist in the trie
            if (!current.IsEndOfWord)
            {
                return false;
            }

            // Set IsEndOfWord to false to mark the word as deleted
            current.IsEndOfWord = false;

            // Check if the current node has any children
            if (current.Children.Count == 0)
            {
                // If the current node has no children, it can be safely removed from the trie
                return true;
            }

            // If the current node has children, it is part of another word, so we don't delete it
            return false;
        }

        char c = word[index];

        // Recursive case: Delete the next character in the word
        if (_delete(current.Children[c], word, index + 1))
        {
            // If the child node was deleted, remove the reference to it from the current node
            current.Children.Remove(c);

            // Check if the current node has any children
            if (current.Children.Count == 0 && !current.IsEndOfWord)
            {
                // If the current node has no children and is not the end of a word, it can be safely removed from the trie
                return true;
            }
        }

        return false;
    }
    
    public bool Delete(string word)
    {
        return _delete(root, word, 0);
    }
