# Snapshot

Snapshot aims to reduce the complexity of unit tests, and provide test documentation in the form of JSON files.

It is an implementation of the Jest Snapshot Testing concept that is found in [React Javascript unit testing](https://facebook.github.io/jest/docs/en/snapshot-testing.html).

## Setup

First install the NuGet package `Snapshot`. Currently only a pre-release version exists: `0.1.0-alpha`.

Next use `SnapshotAssert` to generate Snapshot files. See [these tests](Snapshot.Example/ExampleTests.cs) for an example of the expected behaviour.

## Use Cases

* Testing the output of mapping logic, replace multiple assertions with a single snapshot.
* Asserting that there are no changes in the response of an API.

## Known Issues
* Not possible to update a snapshot programmatically.
* No test runner output / warnings.
* Calling `.Snapshot(..)` from a non-Fact/Theory method will use that method name for the naming of the snapshot JSON file. This can cause issues when using that non-Fact/Theory method for multiple tests.

## Contributing
See [Contributing.md](Contributing.md).