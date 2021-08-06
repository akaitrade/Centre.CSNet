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
  public partial class SingleQuery : TBase
  {
    private byte[] _requestedAddress;
    private TransactionId _fromId;
    private List<SingleTokenQuery> _tokensList;

    public byte[] RequestedAddress
    {
      get
      {
        return _requestedAddress;
      }
      set
      {
        __isset.requestedAddress = true;
        this._requestedAddress = value;
      }
    }

    public TransactionId FromId
    {
      get
      {
        return _fromId;
      }
      set
      {
        __isset.fromId = true;
        this._fromId = value;
      }
    }

    public List<SingleTokenQuery> TokensList
    {
      get
      {
        return _tokensList;
      }
      set
      {
        __isset.tokensList = true;
        this._tokensList = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool requestedAddress;
      public bool fromId;
      public bool tokensList;
    }

    public SingleQuery() {
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
                RequestedAddress = iprot.ReadBinary();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Struct) {
                FromId = new TransactionId();
                FromId.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.List) {
                {
                  TokensList = new List<SingleTokenQuery>();
                  TList _list49 = iprot.ReadListBegin();
                  for( int _i50 = 0; _i50 < _list49.Count; ++_i50)
                  {
                    SingleTokenQuery _elem51;
                    _elem51 = new SingleTokenQuery();
                    _elem51.Read(iprot);
                    TokensList.Add(_elem51);
                  }
                  iprot.ReadListEnd();
                }
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
        TStruct struc = new TStruct("SingleQuery");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (RequestedAddress != null && __isset.requestedAddress) {
          field.Name = "requestedAddress";
          field.Type = TType.String;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteBinary(RequestedAddress);
          oprot.WriteFieldEnd();
        }
        if (FromId != null && __isset.fromId) {
          field.Name = "fromId";
          field.Type = TType.Struct;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          FromId.Write(oprot);
          oprot.WriteFieldEnd();
        }
        if (TokensList != null && __isset.tokensList) {
          field.Name = "tokensList";
          field.Type = TType.List;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.Struct, TokensList.Count));
            foreach (SingleTokenQuery _iter52 in TokensList)
            {
              _iter52.Write(oprot);
            }
            oprot.WriteListEnd();
          }
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
      StringBuilder __sb = new StringBuilder("SingleQuery(");
      bool __first = true;
      if (RequestedAddress != null && __isset.requestedAddress) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RequestedAddress: ");
        __sb.Append(RequestedAddress);
      }
      if (FromId != null && __isset.fromId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("FromId: ");
        __sb.Append(FromId== null ? "<null>" : FromId.ToString());
      }
      if (TokensList != null && __isset.tokensList) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("TokensList: ");
        __sb.Append(TokensList);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
