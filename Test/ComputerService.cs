using Microsoft.Data.SqlClient;
using Test.Models;

namespace Test;

public class ComputerService: IComputerService
{
    private readonly string _connectionString;

    public ComputerService(string connectionString)
    {
        _connectionString = connectionString;

    }

    public int AddCpu(Cpu cpu)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        const string insertString = @"INSERT INTO CPU(Name, Frequency, Cores) VALUES (@Name, @Frequency, @Cores)";

        using var command = new SqlCommand(insertString, connection);
        command.Parameters.AddWithValue("Name", cpu.Name);
        command.Parameters.AddWithValue("Frequency", cpu.Frequency);
        command.Parameters.AddWithValue("Cores", cpu.Cores);

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected <= 0) throw new Exception("Failed to insert data.");
          
        var result = command.ExecuteScalar();

        if (result == DBNull.Value) throw new Exception("Failed to insert data.");
        var id = Convert.ToInt32(result);
        return id;
    }
    
    public int AddVideocard(Videocard videocard)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        const string insertString = "INSERT INTO Videocard(Name, Frequency, Memory) " +
                                        "VALUES (@Name, @Frequency, @Memory)";

        using var command = new SqlCommand(insertString, connection);
        command.Parameters.AddWithValue("Name", videocard.Name);
        command.Parameters.AddWithValue("Frequency", videocard.Frequency);
        command.Parameters.AddWithValue("Memory", videocard.Memory);

        var rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected <= 0) throw new Exception("Failed to insert data.");
          
        var result = command.ExecuteScalar();

        if (result == DBNull.Value) throw new Exception("Failed to insert data.");
        var id = Convert.ToInt32(result);
        return id;
    }
    
    public int AddComputer(string cpu, string videocard, string name)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        const string insertString = "INSERT INTO Computer(VideocardId, CPUId, Name " +
                                    "VALUES (@VideocardId, @CPUId, @Name)";

        const string videocardIdCommand = "select Id from Videocard where Name = @videocard";
        SqlCommand command1 = new(videocardIdCommand, connection);
        var videocardId = command1.ExecuteNonQuery();
        
        const string cpuIdCommand = "select Id from CPU where Name = @cpu";
        SqlCommand command2 = new(cpuIdCommand, connection);
        var cpuId = command2.ExecuteNonQuery();

        if (cpuId == 0 || videocardId == 0) return 0;
        
        using var command = new SqlCommand(insertString, connection);
        command.Parameters.AddWithValue("VideocardId", videocardId); 
        command.Parameters.AddWithValue("Frequency", cpuId);
        command.Parameters.AddWithValue("Name", name);

        var newId = Convert.ToInt32(command.ExecuteScalar()); 
        Console.WriteLine(newId);
        return newId;
    }

    public bool DeleteComputer(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        const string deleteString = "delete from Computer"+
                                    "where Id = @Id";

        using var command = new SqlCommand(deleteString, connection);
        command.ExecuteNonQuery();
        return true;
    }
}