using System.Diagnostics;
using System.Runtime.ExceptionServices;

Stopwatch stopwatch = Stopwatch.StartNew();

// usage 100 thousand values
stopwatch.Start();
int[] largeArr = GenerateRandomArray(100000, 1, 1000);
stopwatch.Stop();
DisplayRuntime(stopwatch);

//Part:1 arrays
int[] bubArr1 = largeArr;
int[] insrtArr1 = largeArr;
int[] mrgArr1 = largeArr;
int[] qckArr1 = largeArr;

// Write your function to test each algorithm here

// SORT DISPLAY HERE

Console.WriteLine("Original Array\n");
Console.WriteLine(StringArray(largeArr));

//bubble sort
string bubt = timeTest(bubArr1, 0, bubArr1.Length - 1, bubbleSort, "Bubble Sort");

//insert sort
string insrtT = timeTest(insrtArr1, 0, insrtArr1.Length - 1, insertionSort, "Insert Sort");

//merge sort
string mrgT = timeTest(mrgArr1, 0, mrgArr1.Length - 1, mergeSort, "Merge Sort");

//quick sort
string qckT = timeTest(qckArr1, 0, qckArr1.Length - 1, QuickSort, "Quick Sort");

//"spreadsheet"
Console.WriteLine("\n\n\n\n");
Console.WriteLine("{0, 10}{1, 20}{2, 20}{3, 20}", 
    "Bubble Sort",
    "Insert Sort",
    "Merge Sort",
    "Quick Sort"
    );
Console.WriteLine("{0, 10}{1, 20}{2, 20}{3, 20}",
    bubt,
    insrtT,
    mrgT,
    qckT
    );

string timeTest(int[] arr, int start, int end, Func <int[] , int,  int, int[]> sort, string sName)
{
    Console.WriteLine("\n " + sName + " \n");

    stopwatch.Start();
    sort(arr, start, end);

    int arrL = arr.Length - 1;

    Console.WriteLine(StringArray(arr));
    Console.WriteLine(DisplayRuntime(stopwatch));
    string output = DisplayRuntime(stopwatch);
    stopwatch.Stop();
    stopwatch.Reset();

    return output;
}

//bubbleSort
//insertionSort
//mergeSort (odd format)
//QuickSort (odd format)

// Write individual functions for each algorithm here (Bubble, Insertion, Merge, and Quick sort)

//Bubble Sort
int[] bubbleSort(int[] arr, int start, int end)
{
    int i, j, temp;
    bool swapped;
    for (i = 0; i < arr.Length - 1; i++)
    {
        swapped = false;
        for (j = 0; j < arr.Length - i - 1; j++)
        {
            if (arr[j] > arr[j + 1])
            {

                // Swap arr[j] and arr[j+1]
                temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
                swapped = true;
            }
        }

        // If no two elements were
        // swapped by inner loop, then break
        if (swapped == false)
            break;
    }
    return arr;
}

//Merge Sort
int[] mergeSort(int[] arr, int start, int end)
{
    if (start < end)
    {

        // Find the middle point
        int m = start + (end - start) / 2;

        // Sort first and second halves
        mergeSort(arr, start, m);
        mergeSort(arr, m + 1, end);

        // Merge the sorted halves
        merge(arr, start, m, end);
    }
    return arr;
}

static int Partition(int[] arr, int low, int high)
{

    // Choose the pivot
    int pivot = arr[high];

    // Index of smaller element and indicates 
    // the right position of pivot found so far
    int i = low - 1;

    // Traverse arr[low..high] and move all smaller
    // elements to the left side. Elements from low to 
    // i are smaller after every iteration
    for (int j = low; j <= high - 1; j++)
    {
        if (arr[j] < pivot)
        {
            i++;
            Swap(arr, i, j);
        }
    }

    // Move pivot after smaller elements and
    // return its position
    Swap(arr, i + 1, high);
    return i + 1;
}

// Swap function
static void Swap(int[] arr, int i, int j)
{
    int temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
}

// The QuickSort function implementation
static int[] QuickSort(int[] arr, int low, int high)
{
    if (low < high)
    {

        // pi is the partition return index of pivot
        int pi = Partition(arr, low, high);

        // Recursion calls for smaller elements
        // and greater or equals elements
        QuickSort(arr, low, pi - 1);
        QuickSort(arr, pi + 1, high);
    }
    return arr;
}

//Merges two sub-arrays
void merge(int[] arr, int l, int m, int r)
{
    // Find sizes of two
    // subarrays to be merged
    int n1 = m - l + 1;
    int n2 = r - m;

    // Create temp arrays
    int[] L = new int[n1];
    int[] R = new int[n2];
    int i, j;

    // Copy data to temp arrays
    for (i = 0; i < n1; ++i)
        L[i] = arr[l + i];
    for (j = 0; j < n2; ++j)
        R[j] = arr[m + 1 + j];

    // Merge the temp arrays

    // Initial indexes of first
    // and second subarrays
    i = 0;
    j = 0;

    // Initial index of merged
    // subarray array
    int k = l;
    while (i < n1 && j < n2)
    {
        if (L[i] <= R[j])
        {
            arr[k] = L[i];
            i++;
        }
        else
        {
            arr[k] = R[j];
            j++;
        }
        k++;
    }

    // Copy remaining elements
    // of L[] if any
    while (i < n1)
    {
        arr[k] = L[i];
        i++;
        k++;
    }

    // Copy remaining elements
    // of R[] if any
    while (j < n2)
    {
        arr[k] = R[j];
        j++;
        k++;
    }
}

int[] insertionSort(int[] arr, int start, int end)
{
    int n = arr.Length;
    for (int i = 1; i < n; ++i)
    {
        int key = arr[i];
        int j = i - 1;

        /* Move elements of arr[0..i-1], that are
           greater than key, to one position ahead
           of their current position */
        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j = j - 1;
        }
        arr[j + 1] = key;
    }
    return arr;
}

// function
static int[] GenerateRandomArray(int length, int minValue, int maxValue)
{
    Random rand = new Random();
    int[] array = new int[length];

    for (int i = 0; i < length; i++)
    {
        array[i] = rand.Next(minValue, maxValue); // Generates a random integer within the specified range
    }

    return array;
}

static string DisplayRuntime(Stopwatch stopwatch)
{
    TimeSpan ts = stopwatch.Elapsed;

    // Format and display the TimeSpan value.
    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
        ts.Hours, ts.Minutes, ts.Seconds,
        ts.Milliseconds / 10);
    //Console.WriteLine("Time Taken: " + elapsedTime);
    return elapsedTime;
}

//toString method for arrays
static string StringArray(int[] arr)
{
    int i = 0;
    int l = arr.Length;
    int[] carr = arr;
    string strarr = "";

    while (i < l)
    {
        strarr = strarr + " " + arr[i];
        i++;
    }
    return strarr;
}