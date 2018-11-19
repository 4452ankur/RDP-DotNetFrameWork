using System;
using System.Collections.Generic;
using System.Text;

namespace RDP_FramWork
{
   public class RdpEmployee
    {
        private string _RdpName;
        private string _RdpId;
        private string _RDPAddress;
        private string _RDPProject;
        private DateTime _RDPDOB;
        private int _RDSalary;

        public String RdpName {
            get { return _RdpName;   }
            set {  _RdpName = value; }
        }
        public string RDPID {
            get {  return _RdpId; }
            set { _RdpId = value; }
        }
        public string RDPAddress
        {
            get { return _RDPAddress; }
            set { _RDPAddress = value; }
        }
        public string RDPProject
        {
            get { return _RDPProject; }
            set { _RDPProject = value; }
        }
        public DateTime RDPDOB
        {
            get { return _RDPDOB; }
            set { _RDPDOB = value; }
        }
        public int RDSalary
        {
            get { return _RDSalary; }
            set { _RDSalary = value; }
        }


    }
}
