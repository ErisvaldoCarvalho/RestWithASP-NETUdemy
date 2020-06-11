using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASP_NETUdemy.Model
{
    public class Person
    {
		private long? id;

		public long? Id
		{
			get { return id; }
			set { id = value; }
		}

		private string firstName;

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		private string lastName;

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		private string address;

		public string Address
		{
			get { return address; }
			set { address = value; }
		}

		private string gender;

		public string Gender
		{
			get { return gender; }
			set { gender = value; }
		}

	}
}
