using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InapescaWeb.DAL;
using InapescaWeb.Entidades;

namespace InapescaWeb.BRL
{
    public class MngNegocioActualizaDatos
    {

        public static Boolean Update_ActualizaDatos(string psclv_nombre, string psclv_ap_pat, string psclv_ap_mat, string psclv_fecha_nac, string psclv_calle, string psclv_numext, string psclv_num_int, string psclv_colonia, string psclv_delegacion, string psclv_rfc, string psclv_curp, string psclv_email, string psCD, string psclv_estado)
        { 
            return MngActualizaDatos.Update_ActualizaDatos(psclv_nombre, psclv_ap_pat, psclv_ap_mat, psclv_fecha_nac, psclv_calle, psclv_numext, psclv_num_int, psclv_colonia, psclv_delegacion, psclv_rfc, psclv_curp, psclv_email, psCD, psclv_estado); 
        }

        public static Boolean Update_ActualizaDatosUser(Usuario poUsuario, string psUsuario)
        {
            return MngActualizaDatos.Update_ActualizaDatosUser(poUsuario, psUsuario);
        }
        public static Boolean Update_ActualizaCuentaBancaria(string psUsuario, string psCuentaBancaria)
        {
            return MngActualizaDatos.Update_ActualizaCuentaBancaria(psUsuario, psCuentaBancaria);
        }

        public static Boolean Inserta_Datos(string psusuario,string psNOMBRE,string psAP_PAT,string PSAP_MAT,string psABREVIATURA,string PSGRADO_ACADEMICO,string PSTITULO,string PSFECH_NAC,string PSCALLE,string PSNUMEXT,string PSNUM_INT,string PSCOLONIA,string PSDELEGACION,string psCD,string PSCLV_ESTADO,string PSCLV_PAIS,string PSRFC,string PSCURP,string PSEMAIL)
        {
            return MngActualizaDatos.Insert_Datos(psusuario,psNOMBRE,psAP_PAT,PSAP_MAT,psABREVIATURA,PSGRADO_ACADEMICO,PSTITULO,PSFECH_NAC,PSCALLE,PSNUMEXT,PSNUM_INT, PSCOLONIA,PSDELEGACION,psCD,PSCLV_ESTADO,PSCLV_PAIS,PSRFC,PSCURP,PSEMAIL);
        }
        
        public static Usuario Obten_DatosUser(string psUsuario, string  psdep,string psYear )
        {
          //  return MngActualizaDatos.Obten_DatosUser(psUsuario, psdep);
            return MngActualizaDatos.Obten_DatosUser(psUsuario, psdep,psYear );
        }
       
        
    }
}
