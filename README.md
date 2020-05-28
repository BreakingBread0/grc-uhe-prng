# grc-uhe-prng
GRC's Ultra-High Entropy Pseudo-Random Number Generator in C#. There was no C# (or any other version from python or js) avaliable, so I made it myself. Hope it saves someone the hassle of doing it himslef.

# Usage

```cs
NewPRNG prng = new NewPRNG("test123");
Console.WriteLine("First: " + prng.random());
Console.WriteLine("Second: " + prng.random());
Console.WriteLine("Third: " + prng.random());
```
Expected C# Output:
```
First: 0.933336379350607
Second: 0.111061654529096
Third: 0.739756175219751
```
JavaScript Output:
```
0.9333363793506073
0.11106165452909555
0.739756175219751
```
<sup>NOTE: Console.WriteLine (C#) and console.log (JS) format double precision integers differently.</sup>

# References

https://github.com/wuftymerguftyguff/uheprng

https://github.com/sukima/grc-uheprng
