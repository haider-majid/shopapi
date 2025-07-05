# Redis Caching Implementation

This document describes the Redis caching implementation for the Store API.

## Overview

Redis caching has been implemented for all "Get All" operations across the following entities:
- Products
- Categories  
- Accounts

## Features

### Cache Operations
- **Get All**: Cached with 30-minute expiration by default
- **Cache Invalidation**: Automatically invalidated on Create, Update, Delete operations
- **Key Generation**: Consistent key naming convention: `{entity}:{operation}`

### Cache Keys
- Products: `product:getall`
- Categories: `category:getall`
- Accounts: `account:getall`

## Configuration

### Redis Connection
The Redis connection is configured in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "Redis": "localhost:6379"
  }
}
```

### Default Settings
- **Instance Name**: `StoreAPI_`
- **Default Expiration**: 30 minutes
- **Fallback Connection**: `localhost:6379`

## Implementation Details

### ICacheService Interface
```csharp
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task RemoveByPatternAsync(string pattern);
    string GenerateKey(string entity, string operation, params object[] parameters);
}
```

### Service Integration
All services now include:
1. **Cache-first approach** for GetAll operations
2. **Automatic cache invalidation** on data modifications
3. **JSON serialization** for complex objects

### Cache Flow
1. **Get All Request** → Check Redis Cache
2. **Cache Hit** → Return cached data
3. **Cache Miss** → Query Database → Cache Result → Return data
4. **Data Modification** → Invalidate related cache

## Setup Instructions

### 1. Install Redis
```bash
# macOS (using Homebrew)
brew install redis

# Start Redis
brew services start redis

# Or run manually
redis-server
```

### 2. Verify Redis Connection
```bash
redis-cli ping
# Should return: PONG
```

### 3. Run the Application
```bash
dotnet run
```

## Performance Benefits

- **Reduced Database Load**: Frequently accessed data served from memory
- **Faster Response Times**: Cache hits return data in milliseconds
- **Scalability**: Redis can handle high concurrent access
- **Automatic Expiration**: Prevents stale data issues

## Monitoring

### Redis CLI Commands
```bash
# Monitor Redis operations
redis-cli monitor

# Check memory usage
redis-cli info memory

# List all keys
redis-cli keys "*"

# Check specific cache keys
redis-cli keys "StoreAPI_*"
```

## Troubleshooting

### Common Issues
1. **Redis Connection Failed**: Ensure Redis server is running
2. **Cache Not Working**: Check connection string in appsettings.json
3. **Memory Issues**: Monitor Redis memory usage and adjust expiration times

### Debug Cache Operations
Enable debug logging in `appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Application.Services": "Debug"
    }
  }
}
```

## Future Enhancements

- **Cache Warming**: Pre-populate cache on application startup
- **Distributed Caching**: Support for Redis cluster
- **Cache Statistics**: Monitor cache hit/miss ratios
- **Conditional Caching**: Cache based on request parameters 