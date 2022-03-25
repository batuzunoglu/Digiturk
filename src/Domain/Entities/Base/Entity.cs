using System;

namespace Domain.Entities.Base
{
	public abstract class Entity<TPrimaryKey> where TPrimaryKey : IEquatable<TPrimaryKey>
	{
		public virtual TPrimaryKey Id { get; set; }
	}
}
