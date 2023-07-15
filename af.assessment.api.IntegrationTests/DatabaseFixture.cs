/*
 * [2019] - [2021] Eblocks Software (Pty) Ltd, All Rights Reserved.
 * NOTICE: All information contained herein is, and remains the property of Eblocks
 * Software (Pty) Ltd.
 * and its suppliers (if any). The intellectual and technical concepts contained herein
 * are proprietary
 * to Eblocks Software (Pty) Ltd. and its suppliers (if any) and may be covered by South 
 * African, U.S.
 * and Foreign patents, patents in process, and are protected by trade secret and / or 
 * copyright law.
 * Dissemination of this information or reproduction of this material is forbidden unless
 * prior written
 * permission is obtained from Eblocks Software (Pty) Ltd.
*/

using System;
using Microsoft.EntityFrameworkCore;
using Xunit;

using af.assessment.api.Data;

namespace af.assessment.api.IntegrationTests
{
    /// <summary>
    ///     
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        /// <summary>
        ///     A <see cref="VaccineDbContext"/> representing the database context to be used.
        /// </summary>
        private readonly VaccineDbContext _dbContext;
        
        /// <summary>
        ///     A <see cref="bool"/> representings whether the fixture is disposed.
        /// </summary>
        private bool disposedValue;

        /// <summary>
        ///     Initializes a new instance of the class <see cref="DatabaseFixture"/>.
        /// </summary>
        public DatabaseFixture()
        {
            var builder = new DbContextOptionsBuilder<VaccineDbContext>();

            builder.UseNpgsql(Environment.GetEnvironmentVariable("CUSTOMCONNSTR_POSTGRES"));
            _dbContext = new VaccineDbContext(builder.Options);

            _dbContext.Database.Migrate();
        }

        /// <summary>
        ///     Disposes the database fixture if the given value is true.
        /// </summary>
        /// <param name="disposing">
        ///     A <see cref="bool"/> representing whether the fixture should be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Database.EnsureDeleted();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        ///     Calls the <see cref="Dispose"/> method.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    ///     A placeholder class to apply [CollectionDefinition] and the ICollectionFixture<> interfaces.
    /// </summary>
    [CollectionDefinition("Database")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}