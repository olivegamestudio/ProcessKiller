# ProcessKiller
Kills a process by the specified name, a number of times and at set intervals.

## Usage

```
ProcessKiller -p ProcessName -n NumTimes -i interval
```

## Example

Kill the explorer process 10 times with a wait of 1000ms between attempts.

```
ProcessKiller -p Explorer -n 10 -i 1000
```
