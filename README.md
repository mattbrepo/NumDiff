# NumDiff
Data comparison tool that calculates and displays the differences between two numeric files.

**Language: C#**

## Why
Comparing two text files is a very common activity in the life of a developer. Sometimes you have to compare files with a lot of doubles and you don't want to be bothered by small differences. For examples when you want 0.30001 to be considered equal to 0.29999. I couldn't find an application that was able to solve this simple problem, so I developed NumDiff.

NumDiff takes two text files with the same number of rows and fields and compare them. The fields can be separated by tab or other characters. In case of numbers you can adjust the tolerance in the _Options_ dialog.

## Example

File a.txt:
```
ciao,0.30001,5,77.9090
miao,1.00,6,92.3762
bau,32,7,101.2003
```

File b.txt:
```
ciao,0.29999,5,77.9090
miao,1.01,6,92.543
ops...,32,8,101.2003
```

![Example](/images/example.jpg)

