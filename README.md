# SessionC#

*Session-typed concurrent and distributed programming for .NET*

**Fluent Session Programming in C#** (PLACES 2020) [[Paper](https://doi.org/10.48550/arXiv.2004.01325)] - Shunsuke Kimura, Keigo Imai

We propose **SessionC#**, a lightweight session typed library for safe concurrent/distributed programming.
The key features are (1) the improved fluent interface which enables writing communication in chained method calls, by exploiting C#'s `out` variables, and (2) amalgamation of session delegation with `async`/`await`, which materialises session cancellation in a limited form, which we call session intervention.
We show the effectiveness of our proposal via a Bitcoin miner application.

## Code

The lightweight version, which includes only types, protocol combinators, and the endpoint API is summarized based on the paper, is available in the [SessionCSharpLight](./SessionCSharpLight/) directory.

## Requirements

Developed using the .NET SDK 8, supports a variety of operating systems, including Windows, macOS, and Linux.

## Examples

- [Tak calculation](./SessionCSharpExamples/TaraiProtocol/Program.cs)
- [A Travel Agency](./SessionCSharpExamples/TravelAgency/Program.cs)

## Applications

- [A Bitcoin miner](./SessionCSharpApplications/BitcoinNonceCalculator/Program.cs)
- [A parallel http downloader](./SessionCSharpApplications/ParallelHttpDownloader/Program.cs)
- [Parallel polygon clipping](./SessionCSharpApplications/PolygonClippingPipeline/Program.cs)

## Citation

```bibtex
@article{sessioncs,
   title={Fluent Session Programming in C#},
   volume={314},
   ISSN={2075-2180},
   url={http://dx.doi.org/10.4204/EPTCS.314.6},
   DOI={10.4204/eptcs.314.6},
   journal={Electronic Proceedings in Theoretical Computer Science},
   publisher={Open Publishing Association},
   author={Kimura, Shunsuke and Imai, Keigo},
   year={2020},
   month=apr, pages={61–75}
}
```
