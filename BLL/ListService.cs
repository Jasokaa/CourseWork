namespace BLL;
public class ListService<T>
{
    public List<T>? Add(List<T>? list, T input)
    {
        list?.Add(input);
        return list;
    }
    public int GetIndex(List<T>? list, T element)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list), "List is empty.");
        }

        int index = list.FindIndex(x => EqualityComparer<T>.Default.Equals(x, element));
        return index; // -1 if not found
    }
    public List<T> DeleteByIndex(List<T>? list, int index)
    {
        if (list == null)
        {
            throw new Exception("List is empty.");
        }
        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);
            return list;
        }
        else
        {
            throw new Exception("Index out of range.");
        }
    }
    public List<T>? ChangeByIndex(List<T>? list, T data, int index)
    {
        if (index >= 0 && index < list.Count)
        {
            list[index] = data;
            return list;
        }
        else
        {
            throw new Exception("Index out of range.");
        }
    }
    public T GetByIndex(List<T>? list, int index)
    {
        if (index >= 0 && index < list.Count)
        {
            return list[index];
        }
        else
        {
            throw new Exception("Index out of range.");
        }
    }
}