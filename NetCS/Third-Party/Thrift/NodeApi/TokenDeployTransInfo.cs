/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace NodeApi
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class TokenDeployTransInfo : TBase
  {
    private string _name;
    private string _code;
    private int _tokenStandard;
    private SmartOperationState _state;
    private TransactionId _stateTransaction;

    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        __isset.name = true;
        this._name = value;
      }
    }

    public string Code
    {
      get
      {
        return _code;
      }
      set
      {
        __isset.code = true;
        this._code = value;
      }
    }

    public int TokenStandard
    {
      get
      {
        return _tokenStandard;
      }
      set
      {
        __isset.tokenStandard = true;
        this._tokenStandard = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="SmartOperationState"/>
    /// </summary>
    public SmartOperationState State
    {
      get
      {
        return _state;
      }
      set
      {
        __isset.state = true;
        this._state = value;
      }
    }

    public TransactionId StateTransaction
    {
      get
      {
        return _stateTransaction;
      }
      set
      {
        __isset.stateTransaction = true;
        this._stateTransaction = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool name;
      public bool code;
      public bool tokenStandard;
      public bool state;
      public bool stateTransaction;
    }

    public TokenDeployTransInfo() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String) {
                Name = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.String) {
                Code = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                TokenStandard = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.I32) {
                State = (SmartOperationState)iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 5:
              if (field.Type == TType.Struct) {
                StateTransaction = new TransactionId();
                StateTransaction.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("TokenDeployTransInfo");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Name != null && __isset.name) {
          field.Name = "name";
          field.Type = TType.String;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Name);
          oprot.WriteFieldEnd();
        }
        if (Code != null && __isset.code) {
          field.Name = "code";
          field.Type = TType.String;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(Code);
          oprot.WriteFieldEnd();
        }
        if (__isset.tokenStandard) {
          field.Name = "tokenStandard";
          field.Type = TType.I32;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(TokenStandard);
          oprot.WriteFieldEnd();
        }
        if (__isset.state) {
          field.Name = "state";
          field.Type = TType.I32;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32((int)State);
          oprot.WriteFieldEnd();
        }
        if (StateTransaction != null && __isset.stateTransaction) {
          field.Name = "stateTransaction";
          field.Type = TType.Struct;
          field.ID = 5;
          oprot.WriteFieldBegin(field);
          StateTransaction.Write(oprot);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("TokenDeployTransInfo(");
      bool __first = true;
      if (Name != null && __isset.name) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Name: ");
        __sb.Append(Name);
      }
      if (Code != null && __isset.code) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Code: ");
        __sb.Append(Code);
      }
      if (__isset.tokenStandard) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("TokenStandard: ");
        __sb.Append(TokenStandard);
      }
      if (__isset.state) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("State: ");
        __sb.Append(State);
      }
      if (StateTransaction != null && __isset.stateTransaction) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("StateTransaction: ");
        __sb.Append(StateTransaction== null ? "<null>" : StateTransaction.ToString());
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
