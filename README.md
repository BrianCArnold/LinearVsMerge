# Rebasing pull requests, (aka "linear histories") invents situations that never occurred

They aren't historical records, they're falsifications that misrepresent developer actions and intent.

## The actual git histories involved

You want to look at

- [Merge Commit PR Results](https://github.com/BrianCArnold/LinearVsMerge/tree/main_merge) (aka what actually happened)
- [Rebase Commit PR Results](https://github.com/BrianCArnold/LinearVsMerge/tree/main_rebase) (aka the falsified history)

Note that the original commits that were included in both branches can be viewed here:

- [Brian's Change](https://github.com/BrianCArnold/LinearVsMerge/tree/Brian_PR_Fix_Showing_Executable)
- [Fred's Change](https://github.com/BrianCArnold/LinearVsMerge/tree/Fred_PR_Fix_Showing_Executable)

# The Code

The program should take N arguments and print them out.

```
internal class Program
{
  private static void Main(string[] args)
  {
    List<string> TheListOfArgs = new();

    Console.WriteLine($"Next we do some stuff with {nameof(TheListOfArgs)}");

    foreach (var str in args)
    {
      TheListOfArgs.Add(str);
    }

    Console.WriteLine("And then we send the list of args to a function to process (print) them");

    ProcessArgs(TheListOfArgs);
  }

  private static void ProcessArgs(IList<string> args)
  {

    Console.WriteLine("These are the command line args.");
    for (int i = 0; i < args.Count(); i++)
    {
      Console.WriteLine($"- {args[i]}");

    }
  }
}
```

There is a problem, however, the first thing in `args` is the name of the executable.

We shouldn't print the first entry in `args`.

(Note: This is complicated for what it does, but what it does isn't important, it's just a framework we're using to compare rebases and merges)

# How two developers tried to fix it

Brian and Fred both tried to fix it, not aware that the other was working on it.

*This is merely a very simple example, with both 'developers' were attempting to fix the same problem, however it is possible for two developers to be working on unrelated issues, where the changes conflict.*

Both changes were made based on the same initial commit, and both are intended to be reasonable solutions to the same problem.

*even if they aren't, the important thing is that the fixes are made on different lines*

In this case, "Brian" [modified the process of adding arguments to "TheListOfArgs"](https://github.com/BrianCArnold/LinearVsMerge/commit/fea3315bb1848747f55723df73e6f55df8aa1634) to skip adding the first argument to the list, preventing the name of the executable from being included in the first place.

"Fred", on the other hand, chose to allow the name of the executable to be included, and instead [skipped the first item in the list](https://github.com/BrianCArnold/LinearVsMerge/commit/83710bd94164fd974036af14dff9c6af70c421a9) when printing them out.

They then both open pull requests, and both pull requests are merged.

Now the program skips the first actual argument. So, another Developer examines the git history to see what happened.

First, in the case where pull requests are rebased on top of the target branch, then let's look at the case where pull requests are merged into the target branch:

## Rebased Pull Requests

The rebased git log looks like this:

```
    * fd95a3e Skip showing the executable in the list of args. - Brian
    |
    * 2d74cb1 Skip showing the executable in the list of args. - Fred
    |
    * 29a48a7 Initial Commit
```

The investigating developer looks 2d74cb1, and confirms that Fred fixed the issue.

Then the investigator looks at fd95a3e, and sees that Brian went in after Fred, and broke the code.

It appears that Brian made an error.

## Merged Pull Requests

The git log shows us this merge history:

```
    *    c33bf90 Merge pull request #4 from BrianCArnold/Brian_PR_Fix_Showing_Executable
    |\  
    * |  d17d56a Merge pull request #3 from BrianCArnold/Fred_PR_Fix_Showing_Executable
   /| |  
  * | |  83710bd Skip showing the executable in the list of args. - Fred
  | | |  
  | | *  fea3315 Skip showing the executable in the list of args. - Brian
   \|/  
    *    29a48a7 Initial Commit
```

We can now clearly see what happened, they both tried to fix the problem parallel. Interestingly, we also see that Brian actually made the change ***first***. Brian didn't break Fred's code, Fred's code didn't even exist yet when Brian made his change.

We can also checkout the fea3315 commit, and check the code, and it works just fine.

*The rebase deletes this history, instead inventing a new falsified history where Brian broke Fred's code, something that never happened.*

We can also easily examine the differences between each commit and it's parent(s), allowing us to see the change that Brian and Fred made in the context that they made them.

- `git diff c33bf90 d17d56a`: We see exactly how Brian's change was merged.
- `git diff c33bf90 fea3315`: We see the differences that the merge had to consider.
- `git diff fea3315 d17d56a`: We see how the two branches differed before they were merged.

# But merging pull requests won't prevent the bug

In this example, the exact same bug is introduced in both cases, as the pull request always results in the same code result.

The difference is that the git log created by rebasing pull requests causes the git log to show "Brian" making a change after "Fred" did, something that clearly never happened.

Rebasing pull requests doesn't make a git history easier to read, it invents an easier to read lie.
