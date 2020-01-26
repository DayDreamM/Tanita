using System;
using System.ComponentModel;

namespace Tanita.model
{
    class StudentCheckData
    {
        public string Id { get; set; }
        public string activityId { get; set; }
        public string studentId { get; set; }
        public string studentName { get; set; }
        public int studentAge { get; set; }
        public double studentHeight { get; set; }
        public string checkDate { get; set; }
        public string jsonContent { get; set; }
        public int isSynchronizeSuccess { get; set; }
        public string birthday { get; set; }
        public string checkinId
        {
            get;
            set;
        }
        public bool bUpload
        {
            get;
            set;
        }
        public DateTime? LastUploadTime
        {
            get;
            set;
        }
        public string NID
        {
            get;
            set;
        }
        public string DeviceSn
        {
            get;
            set;
        }
        public string MO
        {
            get;
            set;
        }
        public string SN
        {
            get;
            set;
        }
        public string ID
        {
            get;
            set;
        }
        public string TrueName
        {
            get;
            set;
        }
        public DateTime Pdate
        {
            get;
            set;
        }
        public int Bt
        {
            get;
            set;
        }
        public int GE
        {
            get;
            set;
        }
        public int AG
        {
            get;
            set;
        }
        [Description("身高|10000001|||cm")]
        public decimal Hm
        {
            get;
            set;
        }
        public decimal Pt
        {
            get;
            set;
        }
        [Description("体重|10000002|||kg")]
        public decimal Wk
        {
            get;
            set;
        }
        [Description("脂肪率|10000003|||%")]
        public decimal FW
        {
            get;
            set;
        }
        [Description("脂肪量|10000004|||kg")]
        public decimal fW
        {
            get;
            set;
        }
        [Description("除脂体重|10000005|||kg")]
        public decimal MW
        {
            get;
            set;
        }
        [Description("肌肉量|10000006|||kg")]
        public decimal mW
        {
            get;
            set;
        }
        [Description("肌肉量等级|10000017|||")]
        public int sW
        {
            get;
            set;
        }
        [Description("骨量|10000007|||kg")]
        public decimal bW
        {
            get;
            set;
        }
        [Description("体水份含量|10000008|||kg")]
        public decimal wW
        {
            get;
            set;
        }
        [Description("体内水份率|10000009|||%")]
        public decimal ww
        {
            get;
            set;
        }
        [Description("BMI|10000010|18.5-24.9")]
        public decimal MI
        {
            get;
            set;
        }
        public decimal Sw
        {
            get;
            set;
        }
        [Description("肥胖程度|10000011|0")]
        public decimal OV
        {
            get;
            set;
        }
        [Description("内脏脂肪等级|10000012|1-9")]
        public int IF
        {
            get;
            set;
        }
        [Description("基础代谢率(kj)|10000013|||kj")]
        public decimal rb
        {
            get;
            set;
        }
        [Description("基础代谢率(kcal)|10000014|||KCAL")]
        public decimal rB
        {
            get;
            set;
        }
        [Description("基础代谢率评价|10000015|||")]
        public int rJ
        {
            get;
            set;
        }
        [Description("体内年龄|10000016|||岁")]
        public int rA
        {
            get;
            set;
        }
        public decimal RO
        {
            get;
            set;
        }
        public int gF
        {
            get;
            set;
        }
        public decimal gW
        {
            get;
            set;
        }
        public decimal gf
        {
            get;
            set;
        }
        public decimal gt
        {
            get;
            set;
        }
        public decimal ZF
        {
            get;
            set;
        }
        public string Advice
        {
            get;
            set;
        }
        public decimal YaoWei
        {
            get;
            set;
        }
        public decimal TunWei
        {
            get;
            set;
        }
        public decimal YaoTunBi
        {
            get;
            set;
        }
        public string ReportImgName
        {
            get;
            set;
        }
        public string ReportImgNamePhy
        {
            get;
            set;
        }
        public string ReportImgUrl
        {
            get;
            set;
        }
        public string ReportPdfPhy
        {
            get;
            set;
        }
        public decimal wI
        {
            get;
            set;
        }
        public decimal wO
        {
            get;
            set;
        }
        public decimal wo
        {
            get;
            set;
        }
        public decimal Sf
        {
            get;
            set;
        }
        public decimal SM
        {
            get;
            set;
        }
        public int LP
        {
            get;
            set;
        }
        public int BA
        {
            get;
            set;
        }
        public int BF
        {
            get;
            set;
        }
        public decimal FR
        {
            get;
            set;
        }
        public decimal fR
        {
            get;
            set;
        }
        public decimal MR
        {
            get;
            set;
        }
        public decimal mR
        {
            get;
            set;
        }
        public int SR
        {
            get;
            set;
        }
        public int sR
        {
            get;
            set;
        }
        public decimal FL
        {
            get;
            set;
        }
        public decimal fL
        {
            get;
            set;
        }
        public decimal ML
        {
            get;
            set;
        }
        public decimal mL
        {
            get;
            set;
        }
        public int SL
        {
            get;
            set;
        }
        public int sL
        {
            get;
            set;
        }
        public decimal Fr
        {
            get;
            set;
        }
        public decimal fr
        {
            get;
            set;
        }
        public decimal Mr
        {
            get;
            set;
        }
        public decimal mr
        {
            get;
            set;
        }
        public int Sr
        {
            get;
            set;
        }
        public int sr
        {
            get;
            set;
        }
        public decimal Fl
        {
            get;
            set;
        }
        public decimal fl
        {
            get;
            set;
        }
        public decimal Ml
        {
            get;
            set;
        }
        public decimal ml
        {
            get;
            set;
        }
        public int Sl
        {
            get;
            set;
        }
        public int sl
        {
            get;
            set;
        }
        public decimal FT
        {
            get;
            set;
        }
        public decimal fT
        {
            get;
            set;
        }
        public decimal MT
        {
            get;
            set;
        }
        public decimal mT
        {
            get;
            set;
        }
        public int ST
        {
            get;
            set;
        }
        public int sT
        {
            get;
            set;
        }
        public decimal aH
        {
            get;
            set;
        }
        public decimal cH
        {
            get;
            set;
        }
        public decimal GH
        {
            get;
            set;
        }
        public decimal HH
        {
            get;
            set;
        }
        public decimal RH
        {
            get;
            set;
        }
        public decimal XH
        {
            get;
            set;
        }
        public decimal JH
        {
            get;
            set;
        }
        public decimal KH
        {
            get;
            set;
        }
        public decimal LH
        {
            get;
            set;
        }
        public decimal QH
        {
            get;
            set;
        }
        public decimal iH
        {
            get;
            set;
        }
        public decimal jH
        {
            get;
            set;
        }
        public decimal aR
        {
            get;
            set;
        }
        public decimal cR
        {
            get;
            set;
        }
        public decimal GR
        {
            get;
            set;
        }
        public decimal HR
        {
            get;
            set;
        }
        public decimal RR
        {
            get;
            set;
        }
        public decimal XR
        {
            get;
            set;
        }
        public decimal JR
        {
            get;
            set;
        }
        public decimal KR
        {
            get;
            set;
        }
        public decimal LR
        {
            get;
            set;
        }
        public decimal QR
        {
            get;
            set;
        }
        public decimal iR
        {
            get;
            set;
        }
        public decimal jR
        {
            get;
            set;
        }
        public decimal aL
        {
            get;
            set;
        }
        public decimal cL
        {
            get;
            set;
        }
        public decimal GL
        {
            get;
            set;
        }
        public decimal HL
        {
            get;
            set;
        }
        public decimal RL
        {
            get;
            set;
        }
        public decimal XL
        {
            get;
            set;
        }
        public decimal JL
        {
            get;
            set;
        }
        public decimal KL
        {
            get;
            set;
        }
        public decimal LL
        {
            get;
            set;
        }
        public decimal QL
        {
            get;
            set;
        }
        public decimal iL
        {
            get;
            set;
        }
        public decimal jL
        {
            get;
            set;
        }
        public decimal ar
        {
            get;
            set;
        }
        public decimal cr
        {
            get;
            set;
        }
        public decimal Gr
        {
            get;
            set;
        }
        public decimal Hr
        {
            get;
            set;
        }
        public decimal Rr
        {
            get;
            set;
        }
        public decimal Xr
        {
            get;
            set;
        }
        public decimal Jr
        {
            get;
            set;
        }
        public decimal Kr
        {
            get;
            set;
        }
        public decimal Lr
        {
            get;
            set;
        }
        public decimal Qr
        {
            get;
            set;
        }
        public decimal ir
        {
            get;
            set;
        }
        public decimal jr
        {
            get;
            set;
        }
        public decimal al
        {
            get;
            set;
        }
        public decimal cl
        {
            get;
            set;
        }
        public decimal Gl
        {
            get;
            set;
        }
        public decimal Hl
        {
            get;
            set;
        }
        public decimal Rl
        {
            get;
            set;
        }
        public decimal Xl
        {
            get;
            set;
        }
        public decimal Jl
        {
            get;
            set;
        }
        public decimal Kl
        {
            get;
            set;
        }
        public decimal Ll
        {
            get;
            set;
        }
        public decimal Ql
        {
            get;
            set;
        }
        public decimal il
        {
            get;
            set;
        }
        public decimal jl
        {
            get;
            set;
        }
        public decimal aF
        {
            get;
            set;
        }
        public decimal cF
        {
            get;
            set;
        }
        public decimal GF
        {
            get;
            set;
        }
        public decimal HF
        {
            get;
            set;
        }
        public decimal RF
        {
            get;
            set;
        }
        public decimal XF
        {
            get;
            set;
        }
        public decimal JF
        {
            get;
            set;
        }
        public decimal KF
        {
            get;
            set;
        }
        public decimal LF
        {
            get;
            set;
        }
        public decimal QF
        {
            get;
            set;
        }
        public decimal iF
        {
            get;
            set;
        }
        public decimal jF
        {
            get;
            set;
        }
        public decimal pH
        {
            get;
            set;
        }
        public decimal pR
        {
            get;
            set;
        }
        public decimal pL
        {
            get;
            set;
        }
        public decimal pr
        {
            get;
            set;
        }
        public decimal pl
        {
            get;
            set;
        }
        public decimal pF
        {
            get;
            set;
        }
        public string UploadErrReason
        {
            get;
            set;
        }
        [Description("分析结果|10000017|||")]
        public string remarkTitle
        {
            get;
            set;
        }
        [Description("健康建议|10000018|||")]
        public string remarkContent
        {
            get;
            set;
        }
    }
}
