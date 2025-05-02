# DNSLG - DNS Load Generator

A lightweight, configurable DNS query load generator for testing DNS resolver performance under various load conditions.

## Overview

DNSLG (DNS Load Generator) is a .NET command-line tool designed to generate controlled DNS query loads with configurable concurrency, intervals, and timeouts. It's useful for:

- Performance testing DNS resolvers
- Stress testing network infrastructure
- Simulating DNS query patterns
- End-to-end testing of systems with DNS dependencies

## Requirements

.NET 8.0 or later

## Installation

Clone the repository and build using .NET CLI:

```
git clone https://github.com/enclave-networks/dnslg.git
cd dnslg
dotnet run
```

## Usage

```
dotnet run -- [options]
```

## Command Line Options

- `--concurrency=N`: Number of concurrent DNS queries per interval (default: 1)
- `--interval=N`: Time in milliseconds between query batches (default: 1000)
- `--timeout=N`: Maximum time in milliseconds to wait for a DNS response (default: 3000)
- `--duration=N`: Test duration in milliseconds (-1 for unlimited, default: -1)
- `--list=FILE`: Path to a file containing hostnames to query (optional)
- `--verbose`: Enable detailed output for each query

## Interactive Controls

While running:

- Press `P` to toggle query scheduling (pause/resume)
- Press `V` to toggle verbose output
- Press `Ctrl+C` to gracefully stop the test

## Example Commands

Basic usage with default settings:

```
dotnet run
```

Run a 30-second test, 100 queries/second loaded at the head of each second:

```
dotnet run -- --list=../dns-source-lists/tranco-list-top-1m.csv --concurrency=100 --interval=1000 --duration=30000
```

Run a 30-second test, 100 queries/second evenly scheduled across the 1 second interval period with an query aggressive timeout:

```
dotnet run -- --list=../dns-source-lists/tranco-list-top-1m.csv --concurrency=5 --interval=50 --duration=30000 --timeout=1000
```

## Hostname Lists

DNSLG requires a list of hostnames to query. You can:

- Provide your own list with `--list=FILE`
- Use default files in the current directory:

    - `dns-source-lists\hosts.txt`
    - `dns-source-lists\tranco-list-top-1m.csv`

### Format requirements:

- One hostname per line
- Lines starting with # are treated as comments
- For multi-column files (like CSV), the last column is used as the hostname


## Output Example

```
C:\Git\dnslg\src>dotnet run
Using DNS source list: C:\Git\dnslg\dns-source-lists\tranco-list-top-1m.csv (limit: 20,000 lines)
Loaded 20,000 hostnames
Press 'P' to toggle scheduling (currently ON). Press 'V' to toggle verbose output. Ctrl+C to stop

  Query rate . . . . : 75 queries/second
  Query timeout. . . : 3,000 ms
  Test duration. . . : 5,000 ms

[13:39:50.476] Total:     15, scheduled: 15, in-flight: 13   avg:    50 ms, success:   1 ( 50%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:50.675] Total:     30, scheduled: 15, in-flight: 27   avg:   297 ms, success:   1 (100%), failed: 0    (no-answer: 0, timeout: 0, exception: 0)
[13:39:50.877] Total:     45, scheduled: 15, in-flight: 38   avg:   580 ms, success:   3 ( 75%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:51.076] Total:     60, scheduled: 15, in-flight: 43   avg:   686 ms, success:   9 ( 90%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:51.278] Total:     75, scheduled: 15, in-flight: 48   avg:   676 ms, success:   6 ( 60%), failed: 4    (no-answer: 4, timeout: 0, exception: 0)
[13:39:51.478] Total:     90, scheduled: 15, in-flight: 60   avg:   611 ms, success:   3 (100%), failed: 0    (no-answer: 0, timeout: 0, exception: 0)
[13:39:51.677] Total:    105, scheduled: 15, in-flight: 74   avg: 1,106 ms, success:   1 (100%), failed: 0    (no-answer: 0, timeout: 0, exception: 0)
[13:39:51.878] Total:    120, scheduled: 15, in-flight: 88   avg: 1,169 ms, success:   1 (100%), failed: 0    (no-answer: 0, timeout: 0, exception: 0)
[13:39:52.078] Total:    135, scheduled: 15, in-flight: 90   avg: 1,312 ms, success:  12 ( 92%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:52.277] Total:    150, scheduled: 15, in-flight: 103  avg:   706 ms, success:   2 (100%), failed: 0    (no-answer: 0, timeout: 0, exception: 0)
[13:39:52.476] Total:    165, scheduled: 15, in-flight: 112  avg: 1,218 ms, success:   5 ( 83%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:52.678] Total:    180, scheduled: 15, in-flight: 120  avg: 1,520 ms, success:   6 ( 86%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:52.878] Total:    195, scheduled: 15, in-flight: 129  avg: 1,523 ms, success:   5 ( 83%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:53.076] Total:    210, scheduled: 15, in-flight: 139  avg: 1,481 ms, success:   2 ( 40%), failed: 3    (no-answer: 3, timeout: 0, exception: 0)
[13:39:53.278] Total:    225, scheduled: 15, in-flight: 147  avg: 1,630 ms, success:   6 ( 86%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:53.478] Total:    240, scheduled: 15, in-flight: 159  avg: 1,588 ms, success:   1 ( 33%), failed: 2    (no-answer: 2, timeout: 0, exception: 0)
[13:39:53.677] Total:    255, scheduled: 15, in-flight: 162  avg: 1,420 ms, success:  11 ( 92%), failed: 1    (no-answer: 1, timeout: 0, exception: 0)
[13:39:53.885] Total:    270, scheduled: 15, in-flight: 172  avg: 1,260 ms, success:   5 (100%), failed: 0    (no-answer: 0, timeout: 0, exception: 0)
[13:39:54.076] Total:    285, scheduled: 15, in-flight: 171  avg: 2,379 ms, success:   7 ( 44%), failed: 9    (no-answer: 0, timeout: 9, exception: 0)
[13:39:54.276] Total:    300, scheduled: 15, in-flight: 169  avg: 2,149 ms, success:   7 ( 41%), failed: 10   (no-answer: 2, timeout: 8, exception: 0)
[13:39:54.477] Total:    315, scheduled: 15, in-flight: 172  avg: 2,690 ms, success:   2 ( 17%), failed: 10   (no-answer: 1, timeout: 9, exception: 0)
[13:39:54.678] Total:    330, scheduled: 15, in-flight: 173  avg: 2,178 ms, success:   7 ( 50%), failed: 7    (no-answer: 0, timeout: 7, exception: 0)
[13:39:54.877] Total:    345, scheduled: 15, in-flight: 174  avg: 2,241 ms, success:   3 ( 21%), failed: 11   (no-answer: 3, timeout: 8, exception: 0)
[13:39:55.077] Total:    360, scheduled: 15, in-flight: 166  avg: 2,351 ms, success:   8 ( 35%), failed: 15   (no-answer: 1, timeout: 14, exception: 0)
[13:39:55.277] Total:    375, scheduled: 15, in-flight: 175  avg: 2,168 ms, success:   2 ( 33%), failed: 4    (no-answer: 1, timeout: 3, exception: 0)
Test duration elapsed, shutting down...
Finished.
```