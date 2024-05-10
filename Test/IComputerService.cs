using Test.Models;

namespace Test;

public interface IComputerService
{
    int AddCpu(Cpu cpu);
    int AddVideocard(Videocard videocard);
    int AddComputer(string cpu, string videocard, string name);

    bool DeleteComputer(int id);
}