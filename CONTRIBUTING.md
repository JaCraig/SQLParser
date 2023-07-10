Contribution Guide
==========================================

## Bugs

Be sure to search for existing issues first. Include code example for reproducing the problem along with expected output. Also include your OS and version of .Net you are using.

## Feature Requests

Please use the Issues system in GitHub to make a feature request and label it appropriately.

## Submitting a Pull Request

There are very few rules/guidelines but a number of "Rules of Thumb" when contributing to the project. The main items to keep in mind:

1. The library is Apache licensed. As such if the code that you are submitting is from another library that uses a stricter license (GPL, etc), then that code will be rejected outright.
2. While extension methods, simple additions, etc. are pretty straightforward, if you plan to work on something fairly large then please contact me first. This allows me to give you feedback and let you know if I like the idea prior to you spending your valuable time on something that may not end up getting merged.
3. For the "Rules of Thumb" that may require some rewrite of your code when submitting, see below.
4. When making a pull request, the system should run a couple of simple checks which will need to pass prior to me reviewing the code.

## Rules of Thumb For External/Internal Libraries
* Reduce the number of libraries that you're using to the bare minimum.
* Always go through a round of simplification and look for code duplication.
* Have speed based tests for all third party libraries and internal types where appropriate.
* Make sure compilation is possible cross platform where at all possible.
* When possible, treat warnings as errors.
* Make sure to run static analysis of the code base and fix any/all errors that are found.

## Rules of Thumb For Classes
* Constructors need to be IoC friendly.
* All public functions should return a value. If it would normally be void, return the object itself unless it doesn't make sense.
* Where possible, make properties read only.
* Aim for 100% test coverage.
* Respect the nullable warnings and try to reduce them where possible.
* Aim for thread safety when possible.
* Where appropriate, make items async. (Especially IO or network based operations)
* All async calls should have Async as the suffix.
* Favor extension methods over static methods when public. Favor static methods over extension methods when private/internal.
* Comment all functions, properties, etc. with enough information that documentation can be generated from it.
* All parameters to a function should supply a default if null is passed in. In otherwords, always do null checks and act accordingly. Do not simply throw an exception or wait for one to happen in the code unless it is appropriate.

## Ways to Contribute

There are many ways you can contribute to the project:

* Fix a bug or implement a new feature (see above).
* Write documentation and help keep it up to date.

## Contributor License Agreement

By contributing your code to Craig's Utility Library you grant James Craig a non-exclusive, irrevocable, worldwide, royalty-free, sublicenseable, transferable license under all of Your relevant intellectual property rights (including copyright, patent, and any other rights), to use, copy, prepare derivative works of, distribute and publicly perform and display the Contributions on any licensing terms, including without limitation: (a) open source licenses like the MIT license; and (b) binary, proprietary, or commercial licenses. Except for the licenses granted herein, You reserve all right, title, and interest in and to the Contribution.

You confirm that you are able to grant us these rights. You represent that You are legally entitled to grant the above license. If Your employer has rights to intellectual property that You create, You represent that You have received permission to make the Contributions on behalf of that employer, or that Your employer has waived such rights for the Contributions.

You represent that the Contributions are Your original works of authorship, and to Your knowledge, no other person claims, or has the right to claim, any right in any invention or patent related to the Contributions. You also represent that You are not legally obligated, whether by entering into an agreement or otherwise, in any way that conflicts with the terms of this license.

James Craig acknowledges that, except as explicitly described in this Agreement, any Contribution which you provide is on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING, WITHOUT LIMITATION, ANY WARRANTIES OR CONDITIONS OF TITLE, NON-INFRINGEMENT, MERCHANTABILITY, OR FITNESS FOR A PARTICULAR PURPOSE.
